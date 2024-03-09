namespace AudioSwitcher.AudioApi.Session;

public class SessionMuteChangedArgs
{
    public SessionMuteChangedArgs(IAudioSession session, bool isMuted)
    {
        Session = session;
        IsMuted = isMuted;
    }

    public IAudioSession Session { get; private set; }

    public bool IsMuted { get; private set; }
}