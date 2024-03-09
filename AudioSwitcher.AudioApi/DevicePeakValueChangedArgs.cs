namespace AudioSwitcher.AudioApi;

public class DevicePeakValueChangedArgs : DeviceChangedArgs
{
    public DevicePeakValueChangedArgs(IDevice device, double peakValue)
        : base(device, DeviceChangedType.PeakValueChanged)
    {
        PeakValue = peakValue;
    }

    public double PeakValue { get; private set; }
}