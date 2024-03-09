namespace AudioSwitcher.AudioApi;

public class DeviceMuteChangedArgs : DeviceChangedArgs
{
    public DeviceMuteChangedArgs(IDevice device, bool isMuted)
        : base(device, DeviceChangedType.MuteChanged)
    {
        IsMuted = isMuted;
    }

    public bool IsMuted { get; private set; }
}