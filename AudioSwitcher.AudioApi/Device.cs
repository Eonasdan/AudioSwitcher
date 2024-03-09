using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AudioSwitcher.AudioApi.Observables;

namespace AudioSwitcher.AudioApi;

/// <summary>
/// Provides a basic implementation of IDevice so that developers need not implement very common functionality.
/// Should be used in place of IDevice where possible
/// </summary>
public abstract class Device : IDevice
{
    private readonly Broadcaster<DefaultDeviceChangedArgs> _defaultChanged;
    private readonly Broadcaster<DeviceMuteChangedArgs> _muteChanged;
    private readonly Broadcaster<DevicePeakValueChangedArgs> _peakValueChanged;
    private readonly Broadcaster<DevicePropertyChangedArgs> _propertyChanged;
    private readonly Broadcaster<DeviceStateChangedArgs> _stateChanged;
    private readonly Broadcaster<DeviceVolumeChangedArgs> _volumeChanged;

    protected Device(IAudioController controller)
    {
        Controller = controller;
        _muteChanged = new Broadcaster<DeviceMuteChangedArgs>();
        _stateChanged = new Broadcaster<DeviceStateChangedArgs>();
        _volumeChanged = new Broadcaster<DeviceVolumeChangedArgs>();
        _defaultChanged = new Broadcaster<DefaultDeviceChangedArgs>();
        _propertyChanged = new Broadcaster<DevicePropertyChangedArgs>();
        _peakValueChanged = new Broadcaster<DevicePeakValueChangedArgs>();
    }

    public IAudioController Controller { get; }

    public virtual IObservable<DefaultDeviceChangedArgs> DefaultChanged => _defaultChanged.AsObservable();

    public abstract DeviceType DeviceType { get; }

    public abstract string FullName { get; }

    public abstract IEnumerable<IDeviceCapability> GetAllCapabilities();

    public abstract TCapability GetCapability<TCapability>() where TCapability : IDeviceCapability;

    public virtual Task<double> GetVolumeAsync()
    {
        return GetVolumeAsync(CancellationToken.None);
    }

    public abstract Task<double> GetVolumeAsync(CancellationToken cancellationToken);

    public abstract bool HasCapability<TCapability>() where TCapability : IDeviceCapability;

    public abstract DeviceIcon Icon { get; }
    public abstract string IconPath { get; }

    public abstract Guid Id { get; }

    public abstract string InterfaceName { get; }

    public virtual bool IsCaptureDevice => DeviceType == DeviceType.Capture || DeviceType == DeviceType.All;

    public abstract bool IsDefaultCommunicationsDevice { get; }

    public abstract bool IsDefaultDevice { get; }

    public abstract bool IsMuted { get; }

    public virtual bool IsPlaybackDevice => DeviceType == DeviceType.Playback || DeviceType == DeviceType.All;

    public virtual IObservable<DeviceMuteChangedArgs> MuteChanged => _muteChanged.AsObservable();

    public abstract string Name { get; set; }

    public virtual IObservable<DevicePeakValueChangedArgs> PeakValueChanged => _peakValueChanged.AsObservable();

    public virtual IObservable<DevicePropertyChangedArgs> PropertyChanged => _propertyChanged.AsObservable();

    public virtual bool SetAsDefault()
    {
        return SetAsDefault(CancellationToken.None);
    }

    public abstract bool SetAsDefault(CancellationToken cancellationToken);

    public virtual Task<bool> SetAsDefaultAsync()
    {
        return SetAsDefaultAsync(CancellationToken.None);
    }

    public abstract Task<bool> SetAsDefaultAsync(CancellationToken cancellationToken);

    public virtual bool SetAsDefaultCommunications()
    {
        return SetAsDefaultCommunications(CancellationToken.None);
    }

    public abstract bool SetAsDefaultCommunications(CancellationToken cancellationToken);

    public virtual Task<bool> SetAsDefaultCommunicationsAsync()
    {
        return SetAsDefaultCommunicationsAsync(CancellationToken.None);
    }

    public abstract Task<bool> SetAsDefaultCommunicationsAsync(CancellationToken cancellationToken);

    public virtual Task<bool> SetMuteAsync(bool mute)
    {
        return SetMuteAsync(mute, CancellationToken.None);
    }

    public abstract Task<bool> SetMuteAsync(bool mute, CancellationToken cancellationToken);

    public virtual Task<double> SetVolumeAsync(double volume)
    {
        return SetVolumeAsync(volume, CancellationToken.None);
    }

    public abstract Task<double> SetVolumeAsync(double volume, CancellationToken cancellationToken);

    public abstract DeviceState State { get; }

    public virtual IObservable<DeviceStateChangedArgs> StateChanged => _stateChanged.AsObservable();

    public virtual Task<bool> ToggleMuteAsync()
    {
        return ToggleMuteAsync(CancellationToken.None);
    }

    public Task<bool> ToggleMuteAsync(CancellationToken cancellationToken)
    {
        return SetMuteAsync(!IsMuted, cancellationToken);
    }

    public abstract double Volume { get; }

    public virtual IObservable<DeviceVolumeChangedArgs> VolumeChanged => _volumeChanged.AsObservable();

    protected virtual void Dispose(bool disposing)
    {
        _muteChanged.Dispose();
        _stateChanged.Dispose();
        _volumeChanged.Dispose();
        _defaultChanged.Dispose();
        _propertyChanged.Dispose();
        _peakValueChanged.Dispose();
    }

    protected virtual void OnDefaultChanged()
    {
        _defaultChanged.OnNext(new DefaultDeviceChangedArgs(this));
    }

    protected virtual void OnMuteChanged(bool isMuted)
    {
        _muteChanged.OnNext(new DeviceMuteChangedArgs(this, isMuted));
    }

    protected virtual void OnPeakValueChanged(double peakValue)
    {
        _peakValueChanged.OnNext(new DevicePeakValueChangedArgs(this, peakValue));
    }

    protected virtual void OnPropertyChanged(string propertyName)
    {
        _propertyChanged.OnNext(new DevicePropertyChangedArgs(this, propertyName));
    }

    protected virtual void OnStateChanged(DeviceState state)
    {
        _stateChanged.OnNext(new DeviceStateChangedArgs(this, state));
    }

    protected virtual void OnVolumeChanged(double volume)
    {
        _volumeChanged.OnNext(new DeviceVolumeChangedArgs(this, volume));
    }

    ~Device()
    {
        Dispose(false);
    }
}