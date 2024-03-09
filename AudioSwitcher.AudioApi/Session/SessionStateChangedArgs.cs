namespace AudioSwitcher.AudioApi.Session;

public class SessionStateChangedArgs
{
    public SessionStateChangedArgs(IAudioSession session, AudioSessionState state)
    {
        Session = session;
        State = state;
    }

    public IAudioSession Session { get; private set; }

    public AudioSessionState State { get; private set; }
}