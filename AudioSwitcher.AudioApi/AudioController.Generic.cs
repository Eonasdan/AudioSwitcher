using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi.Observables;

namespace AudioSwitcher.AudioApi;

public abstract class AudioController<T> : IAudioController<T>
    where T : class, IDevice
{
    private const DeviceState DefaultDeviceStateFilter =
        DeviceState.Active | DeviceState.Unplugged | DeviceState.Disabled;

    private readonly Broadcaster<DeviceChangedArgs> _audioDeviceChanged;

    protected AudioController()
    {
        _audioDeviceChanged = new Broadcaster<DeviceChangedArgs>();
    }

    public IObservable<DeviceChangedArgs> AudioDeviceChanged => _audioDeviceChanged.AsObservable();

    IDevice IAudioController.DefaultCaptureCommunicationsDevice => DefaultCaptureCommunicationsDevice;

    IDevice IAudioController.DefaultCaptureDevice => DefaultCaptureDevice;

    IDevice IAudioController.DefaultPlaybackCommunicationsDevice => DefaultPlaybackCommunicationsDevice;

    IDevice IAudioController.DefaultPlaybackDevice => DefaultPlaybackDevice;

    IEnumerable<IDevice> IAudioController.GetCaptureDevices()
    {
        return GetCaptureDevices();
    }

    IEnumerable<IDevice> IAudioController.GetCaptureDevices(DeviceState state)
    {
        return GetCaptureDevices(state);
    }

    Task<IEnumerable<IDevice>> IAudioController.GetCaptureDevicesAsync()
    {
        return Task.FromResult(GetCaptureDevices().OfType<IDevice>());
    }

    Task<IEnumerable<IDevice>> IAudioController.GetCaptureDevicesAsync(DeviceState deviceState)
    {
        return Task.FromResult(GetCaptureDevices(deviceState).OfType<IDevice>());
    }

    IDevice IAudioController.GetDefaultDevice(DeviceType deviceType, Role role)
    {
        return GetDefaultDevice(deviceType, role);
    }

    Task<IDevice> IAudioController.GetDefaultDeviceAsync(DeviceType deviceType, Role role)
    {
        return Task.FromResult(GetDefaultDevice(deviceType, role) as IDevice);
    }

    IDevice IAudioController.GetDevice(Guid id)
    {
        return GetDevice(id);
    }

    IDevice IAudioController.GetDevice(Guid id, DeviceState state)
    {
        return GetDevice(id, state);
    }

    Task<IDevice> IAudioController.GetDeviceAsync(Guid id)
    {
        return Task.FromResult(GetDevice(id) as IDevice);
    }

    Task<IDevice> IAudioController.GetDeviceAsync(Guid id, DeviceState state)
    {
        return Task.FromResult(GetDevice(id, state) as IDevice);
    }

    IEnumerable<IDevice> IAudioController.GetDevices(DeviceType deviceType)
    {
        return GetDevices(deviceType);
    }

    IEnumerable<IDevice> IAudioController.GetDevices()
    {
        return GetDevices();
    }

    IEnumerable<IDevice> IAudioController.GetDevices(DeviceState state)
    {
        return GetDevices(state);
    }

    IEnumerable<IDevice> IAudioController.GetDevices(DeviceType deviceType, DeviceState state)
    {
        return GetDevices(deviceType, state);
    }

    Task<IEnumerable<IDevice>> IAudioController.GetDevicesAsync(DeviceType deviceType)
    {
        return Task.FromResult(GetDevices(deviceType).Cast<IDevice>());
    }

    Task<IEnumerable<IDevice>> IAudioController.GetDevicesAsync()
    {
        return Task.FromResult(GetDevices().Cast<IDevice>());
    }

    Task<IEnumerable<IDevice>> IAudioController.GetDevicesAsync(DeviceState state)
    {
        return Task.FromResult(GetDevices(state).Cast<IDevice>());
    }

    Task<IEnumerable<IDevice>> IAudioController.GetDevicesAsync(DeviceType deviceType, DeviceState state)
    {
        return Task.FromResult(GetDevices(deviceType, state).Cast<IDevice>());
    }

    IEnumerable<IDevice> IAudioController.GetPlaybackDevices()
    {
        return GetPlaybackDevices();
    }

    IEnumerable<IDevice> IAudioController.GetPlaybackDevices(DeviceState state)
    {
        return GetPlaybackDevices(state);
    }

    Task<IEnumerable<IDevice>> IAudioController.GetPlaybackDevicesAsync()
    {
        return Task.FromResult(GetPlaybackDevices().Cast<IDevice>());
    }

    Task<IEnumerable<IDevice>> IAudioController.GetPlaybackDevicesAsync(DeviceState deviceState)
    {
        return Task.FromResult(GetPlaybackDevices(deviceState).Cast<IDevice>());
    }

    public virtual T DefaultCaptureCommunicationsDevice => GetDefaultDevice(DeviceType.Capture, Role.Communications);

    public virtual T DefaultCaptureDevice => GetDefaultDevice(DeviceType.Capture, Role.Console | Role.Multimedia);

    public virtual T DefaultPlaybackCommunicationsDevice => GetDefaultDevice(DeviceType.Playback, Role.Communications);

    public virtual T DefaultPlaybackDevice => GetDefaultDevice(DeviceType.Playback, Role.Console | Role.Multimedia);

    public virtual IEnumerable<T> GetCaptureDevices()
    {
        return GetCaptureDevices(DefaultDeviceStateFilter);
    }

    public virtual IEnumerable<T> GetCaptureDevices(DeviceState state)
    {
        return GetDevices(DeviceType.Capture, state);
    }

    public virtual Task<IEnumerable<T>> GetCaptureDevicesAsync()
    {
        return Task.FromResult(GetCaptureDevices(DefaultDeviceStateFilter));
    }

    public virtual Task<IEnumerable<T>> GetCaptureDevicesAsync(DeviceState deviceState)
    {
        return Task.FromResult(GetCaptureDevices(deviceState));
    }

    public abstract T GetDefaultDevice(DeviceType deviceType, Role role);

    public virtual Task<T> GetDefaultDeviceAsync(DeviceType deviceType, Role role)
    {
        return Task.FromResult(GetDefaultDevice(deviceType, role));
    }

    public virtual T GetDevice(Guid id)
    {
        return GetDevice(id, DefaultDeviceStateFilter);
    }

    public abstract T GetDevice(Guid id, DeviceState state);

    public virtual Task<T> GetDeviceAsync(Guid id)
    {
        return Task.FromResult(GetDevice(id));
    }

    public virtual Task<T> GetDeviceAsync(Guid id, DeviceState state)
    {
        return Task.FromResult(GetDevice(id, state));
    }

    public virtual IEnumerable<T> GetDevices()
    {
        return GetDevices(DefaultDeviceStateFilter);
    }

    public IEnumerable<T> GetDevices(DeviceType deviceType)
    {
        return GetDevices(deviceType, DefaultDeviceStateFilter);
    }

    public virtual IEnumerable<T> GetDevices(DeviceState state)
    {
        return GetDevices(DeviceType.All, state);
    }

    public abstract IEnumerable<T> GetDevices(DeviceType deviceType, DeviceState state);

    public virtual Task<IEnumerable<T>> GetDevicesAsync()
    {
        return Task.FromResult(GetDevices());
    }

    public virtual Task<IEnumerable<T>> GetDevicesAsync(DeviceState state)
    {
        return Task.FromResult(GetDevices(state));
    }

    public Task<IEnumerable<T>> GetDevicesAsync(DeviceType deviceType)
    {
        return GetDevicesAsync(deviceType, DefaultDeviceStateFilter);
    }

    public virtual Task<IEnumerable<T>> GetDevicesAsync(DeviceType deviceType, DeviceState state)
    {
        return Task.FromResult(GetDevices(deviceType, state));
    }

    public virtual IEnumerable<T> GetPlaybackDevices()
    {
        return GetPlaybackDevices(DefaultDeviceStateFilter);
    }

    public virtual IEnumerable<T> GetPlaybackDevices(DeviceState state)
    {
        return GetDevices(DeviceType.Playback, state);
    }

    public virtual Task<IEnumerable<T>> GetPlaybackDevicesAsync()
    {
        return GetPlaybackDevicesAsync(DefaultDeviceStateFilter);
    }

    public virtual Task<IEnumerable<T>> GetPlaybackDevicesAsync(DeviceState deviceState)
    {
        return Task.FromResult(GetPlaybackDevices(deviceState));
    }

    public void Dispose()
    {
        Dispose(true);
    }

    protected virtual void Dispose(bool disposing)
    {
        _audioDeviceChanged.Dispose();
    }

    protected virtual void OnAudioDeviceChanged(DeviceChangedArgs e)
    {
        _audioDeviceChanged.OnNext(e);
    }
}