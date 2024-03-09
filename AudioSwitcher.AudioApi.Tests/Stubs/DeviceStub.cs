namespace AudioSwitcher.AudioApi.Tests.Stubs;

public abstract class DeviceStub : Device
{
    public DeviceStub() : base(null)
    {
    }

    public void FireDefaultChanged()
    {
        OnDefaultChanged();
    }

    public void FireMuteChanged(bool mute)
    {
        OnMuteChanged(mute);
    }

    public void FirePeakChanged(double volume)
    {
        OnPeakValueChanged(volume);
    }

    public void FirePropertyChanged(string name)
    {
        OnPropertyChanged(name);
    }

    public void FireStateChanged(DeviceState state)
    {
        OnStateChanged(state);
    }

    public void FireVolumeChanged(double volume)
    {
        OnVolumeChanged(volume);
    }

    internal void Dispose()
    {
        base.Dispose(true);
    }
}