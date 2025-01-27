﻿namespace AudioSwitcher.AudioApi.Session;

public class SessionVolumeChangedArgs
{
    public SessionVolumeChangedArgs(IAudioSession session, double volume)
    {
        Session = session;
        Volume = volume;
    }

    public IAudioSession Session { get; private set; }

    public double Volume { get; private set; }
}