namespace AudioSwitcher.AudioApi.Session;

public class SessionPeakValueChangedArgs
{
    public SessionPeakValueChangedArgs(IAudioSession session, double peakValue)
    {
        Session = session;
        PeakValue = peakValue;
    }

    public IAudioSession Session { get; private set; }

    public double PeakValue { get; private set; }
}