using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi.Observables;

namespace AudioSwitcher.AudioApi;

public abstract class AudioController : IAudioController
{
    protected const DeviceState DefaultDeviceStateFilter =
        DeviceState.Active | DeviceState.Unplugged | DeviceState.Disabled;

    private readonly Broadcaster<DeviceChangedArgs> _audioDeviceChanged;

    protected AudioController()
    {
        _audioDeviceChanged = new Broadcaster<DeviceChangedArgs>();
    }

    public IObservable<DeviceChangedArgs> AudioDeviceChanged => _audioDeviceChanged.AsObservable();

    public virtual IDevice DefaultCaptureCommunicationsDevice =>
        GetDefaultDevice(DeviceType.Capture, Role.Communications);

    public virtual IDevice DefaultCaptureDevice => GetDefaultDevice(DeviceType.Capture, Role.Console | Role.Multimedia);

    public virtual IDevice DefaultPlaybackCommunicationsDevice =>
        GetDefaultDevice(DeviceType.Playback, Role.Communications);

    public virtual IDevice DefaultPlaybackDevice =>
        GetDefaultDevice(DeviceType.Playback, Role.Console | Role.Multimedia);

    public abstract IEnumerable<IDevice> GetCaptureDevices();

    public abstract IEnumerable<IDevice> GetCaptureDevices(DeviceState deviceState);

    public virtual Task<IEnumerable<IDevice>> GetCaptureDevicesAsync()
    {
        return Task.FromResult(GetCaptureDevices());
    }

    public virtual Task<IEnumerable<IDevice>> GetCaptureDevicesAsync(DeviceState deviceState)
    {
        return Task.FromResult(GetCaptureDevices(deviceState));
    }

    public abstract IDevice GetDefaultDevice(DeviceType deviceType, Role role);

    public virtual Task<IDevice> GetDefaultDeviceAsync(DeviceType deviceType, Role role)
    {
        return Task.FromResult(GetDefaultDevice(deviceType, role));
    }

    public abstract IDevice GetDevice(Guid id);

    public abstract IDevice GetDevice(Guid id, DeviceState state);

    public virtual Task<IDevice> GetDeviceAsync(Guid id)
    {
        return Task.FromResult(GetDevice(id));
    }

    public virtual Task<IDevice> GetDeviceAsync(Guid id, DeviceState state)
    {
        return Task.FromResult(GetDevice(id, state));
    }

    public virtual IEnumerable<IDevice> GetDevices()
    {
        return GetDevices(DefaultDeviceStateFilter);
    }

    public IEnumerable<IDevice> GetDevices(DeviceType deviceType)
    {
        return GetDevices(deviceType, DefaultDeviceStateFilter);
    }

    public virtual IEnumerable<IDevice> GetDevices(DeviceState state)
    {
        return GetDevices(DeviceType.All, state);
    }

    public abstract IEnumerable<IDevice> GetDevices(DeviceType deviceType, DeviceState state);

    public virtual Task<IEnumerable<IDevice>> GetDevicesAsync()
    {
        return Task.FromResult(GetDevices());
    }

    public virtual Task<IEnumerable<IDevice>> GetDevicesAsync(DeviceState state)
    {
        return Task.FromResult(GetDevices(state));
    }

    public Task<IEnumerable<IDevice>> GetDevicesAsync(DeviceType deviceType)
    {
        return GetDevicesAsync(deviceType, DefaultDeviceStateFilter);
    }

    public virtual Task<IEnumerable<IDevice>> GetDevicesAsync(DeviceType deviceType, DeviceState state)
    {
        return Task.FromResult(GetDevices(deviceType, state));
    }

    public abstract IEnumerable<IDevice> GetPlaybackDevices();

    public abstract IEnumerable<IDevice> GetPlaybackDevices(DeviceState deviceState);

    public virtual Task<IEnumerable<IDevice>> GetPlaybackDevicesAsync()
    {
        return Task.FromResult(GetPlaybackDevices());
    }

    public virtual Task<IEnumerable<IDevice>> GetPlaybackDevicesAsync(DeviceState deviceState)
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