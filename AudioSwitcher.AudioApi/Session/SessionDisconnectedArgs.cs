namespace AudioSwitcher.AudioApi.Session;

public class SessionDisconnectedArgs
{
    public SessionDisconnectedArgs(IAudioSession session)
    {
        Session = session;
    }

    public IAudioSession Session { get; private set; }
}