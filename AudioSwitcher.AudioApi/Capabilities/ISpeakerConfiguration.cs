namespace AudioSwitcher.AudioApi.Capabilities;

public interface ISpeakerConfiguration : IDeviceCapability
{
    SpeakerConfiguration Get();
    bool IsSupported(SpeakerConfiguration configuration);

    void Set(SpeakerConfiguration configuration);
}