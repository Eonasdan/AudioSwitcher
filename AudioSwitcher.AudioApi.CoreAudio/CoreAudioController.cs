using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using AudioSwitcher.AudioApi.CoreAudio.Interfaces;
using AudioSwitcher.AudioApi.CoreAudio.Threading;
using AudioSwitcher.AudioApi.Observables;

namespace AudioSwitcher.AudioApi.CoreAudio;

/// <summary>
/// Enumerates Windows System Devices.
/// Stores the current devices in memory to avoid calling the COM library when not required
/// </summary>
public sealed class CoreAudioController : AudioController<CoreAudioDevice>
{
    private readonly ThreadLocal<IMultimediaDeviceEnumerator> _innerEnumerator;
    private readonly ReaderWriterLockSlim _lock = new();
    private HashSet<CoreAudioDevice> _deviceCache = [];

    public CoreAudioController()
    {
        // ReSharper disable once SuspiciousTypeConversion.Global
        var innerEnumerator = ComObjectFactory.GetDeviceEnumerator();
        var innerEnumeratorPtr = Marshal.GetIUnknownForObject(innerEnumerator);

        if (innerEnumerator == null)
            throw new InvalidComObjectException("No Device Enumerator");

        _innerEnumerator = new ThreadLocal<IMultimediaDeviceEnumerator>(() =>
            Marshal.GetUniqueObjectForIUnknown(innerEnumeratorPtr) as IMultimediaDeviceEnumerator);

        ComThread.Invoke(() =>
        {
            SystemEvents = new SystemEventNotifcationClient(() => InnerEnumerator);

            SystemEvents.DeviceAdded.Subscribe(x => OnDeviceAdded(x.DeviceId));
            SystemEvents.DeviceRemoved.Subscribe(x => OnDeviceRemoved(x.DeviceId));

            _deviceCache = [];
            InnerEnumerator.EnumAudioEndpoints(EDataFlow.All, EDeviceState.All, out var collection);

            using var coll = new MultimediaDeviceCollection(collection);
            foreach (var mDev in coll)
                CacheDevice(mDev);
        });
    }

    private IMultimediaDeviceEnumerator InnerEnumerator => _innerEnumerator.Value;

    internal SystemEventNotifcationClient SystemEvents { get; private set; }

    public override CoreAudioDevice GetDefaultDevice(DeviceType deviceType, Role role)
    {
        var devId = GetDefaultDeviceId(deviceType, role);

        var acquiredLock = _lock.AcquireReadLockNonReEntrant();

        try
        {
            return _deviceCache.FirstOrDefault(x => x.RealId == devId);
        }
        finally
        {
            if (acquiredLock)
                _lock.ExitReadLock();
        }
    }

    public override CoreAudioDevice GetDevice(Guid id, DeviceState state)
    {
        var acquiredLock = _lock.AcquireReadLockNonReEntrant();

        try
        {
            return _deviceCache.FirstOrDefault(x => x.Id == id && state.HasFlag(x.State));
        }
        finally
        {
            if (acquiredLock)
                _lock.ExitReadLock();
        }
    }

    public override IEnumerable<CoreAudioDevice> GetDevices(DeviceType deviceType, DeviceState state)
    {
        var acquiredLock = _lock.AcquireReadLockNonReEntrant();

        try
        {
            return _deviceCache.Where(x =>
                (x.DeviceType == deviceType || deviceType == DeviceType.All)
                && state.HasFlag(x.State)).ToList();
        }
        finally
        {
            if (acquiredLock)
                _lock.ExitReadLock();
        }
    }

    protected override void Dispose(bool disposing)
    {
        ComThread.BeginInvoke(() =>
            {
                SystemEvents?.Dispose();
                SystemEvents = null;
            })
            .ContinueWith(_ =>
            {
                foreach (var device in _deviceCache) device.Dispose();

                _deviceCache?.Clear();
                _lock?.Dispose();
                _innerEnumerator?.Dispose();

                base.Dispose(disposing);

                GC.SuppressFinalize(this);
            });
    }

    internal string GetDefaultDeviceId(DeviceType deviceType, Role role)
    {
        InnerEnumerator.GetDefaultAudioEndpoint(deviceType.AsEDataFlow(), role.AsERole(), out var dev);
        if (dev == null)
            return null;

        dev.GetId(out var devId);

        return devId;
    }

    private CoreAudioDevice CacheDevice(IMultimediaDevice mDevice)
    {
        if (!DeviceIsValid(mDevice))
            return null;

        mDevice.GetId(out var id);
        var device = GetDevice(id);

        if (device != null)
            return device;

        device = new CoreAudioDevice(mDevice, this);

        device.StateChanged.Subscribe(OnAudioDeviceChanged);
        device.DefaultChanged.Subscribe(OnAudioDeviceChanged);
        device.PropertyChanged.Subscribe(OnAudioDeviceChanged);

        var lockAcquired = _lock.AcquireWriteLockNonReEntrant();

        try
        {
            _deviceCache.Add(device);
            return device;
        }
        finally
        {
            if (lockAcquired)
                _lock.ExitWriteLock();
        }
    }

    private static bool DeviceIsValid(IMultimediaDevice device)
    {
        try
        {
            device.GetId(out _);
            device.GetState(out _);

            return true;
        }
        catch
        {
            return false;
        }
    }

    private CoreAudioDevice GetDevice(string realId)
    {
        var acquiredLock = _lock.AcquireReadLockNonReEntrant();

        try
        {
            return
                _deviceCache.FirstOrDefault(
                    x => string.Equals(x.RealId, realId, StringComparison.InvariantCultureIgnoreCase));
        }
        finally
        {
            if (acquiredLock)
                _lock.ExitReadLock();
        }
    }

    private CoreAudioDevice GetOrAddDeviceFromRealId(string deviceId)
    {
        //This pre-check here may prevent more com objects from being created
        var device = GetDevice(deviceId);
        if (device != null)
            return device;

        return ComThread.Invoke(() =>
        {
            InnerEnumerator.GetDevice(deviceId, out var mDevice);

            return mDevice == null ? null : CacheDevice(mDevice);
        });
    }

    private void OnDeviceAdded(string deviceId)
    {
        var dev = GetOrAddDeviceFromRealId(deviceId);

        if (dev != null)
            OnAudioDeviceChanged(new DeviceAddedArgs(dev));
    }

    private void OnDeviceRemoved(string deviceId)
    {
        var devicesRemoved = RemoveFromRealId(deviceId);

        foreach (var dev in devicesRemoved)
            OnAudioDeviceChanged(new DeviceRemovedArgs(dev));
    }

    private List<CoreAudioDevice> RemoveFromRealId(string deviceId)
    {
        var lockAcquired = _lock.AcquireWriteLockNonReEntrant();
        try
        {
            var devicesToRemove =
                _deviceCache.Where(
                    x => string.Equals(x.RealId, deviceId, StringComparison.InvariantCultureIgnoreCase)).ToList();

            _deviceCache.RemoveWhere(
                x => string.Equals(x.RealId, deviceId, StringComparison.InvariantCultureIgnoreCase));

            return devicesToRemove;
        }
        finally
        {
            if (lockAcquired)
                _lock.ExitWriteLock();
        }
    }

    ~CoreAudioController()
    {
        Dispose(false);
    }
}