namespace AudioSwitcher.AudioApi;

public class DeviceVolumeChangedArgs : DeviceChangedArgs
{
    public DeviceVolumeChangedArgs(IDevice device, double volume)
        : base(device, DeviceChangedType.VolumeChanged)
    {
        Volume = volume;
    }

    public double Volume { get; private set; }
}