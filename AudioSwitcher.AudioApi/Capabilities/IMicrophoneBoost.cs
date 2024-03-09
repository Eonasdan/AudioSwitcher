namespace AudioSwitcher.AudioApi.Capabilities;

public interface IMicrophoneBoost : IDeviceCapability
{
    int Level { get; }

    int[] GetValidLevels();

    bool IsValidLevel(int level);
}