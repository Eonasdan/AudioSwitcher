﻿namespace AudioSwitcher.AudioApi.CoreAudio.Interfaces;

internal static class ComInterfaceIds
{
    internal const string UNKNOWN_IID = "00000000-0000-0000-C000-000000000046";

    internal const string IMM_DEVICE_IID = "D666063F-1587-4E43-81F1-B948E807363F";
    internal const string IMM_DEVICE_ENUMERATOR_IID = "A95664D2-9614-4F35-A746-DE8DB63617E6";
    internal const string IMM_DEVICE_COLLECTION_IID = "0BD7A1BE-7A1A-44DB-8397-CC5392387B5E";
    internal const string IMM_ENDPOINT_IID = "1BE09788-6894-4089-8586-9A2A6C265AC5";
    internal const string IMM_NOTIFICATION_CLIENT_IID = "7991EEC9-7E89-4D85-8390-6C703CEC60C0";
    internal const string PROPERTY_STORE_IID = "886D8EEB-8CF2-4446-8D02-CDBA1DBDCF99";
    internal const string DEVICE_ENUMERATOR_CID = "BCDE0395-E52F-467C-8E3D-C4579291692E";

    // WASAPI
    internal const string AUDIO_CAPTURE_CLIENT_IID = "C8ADBD64-E71E-48A0-A4DE-185C395CD317";
    internal const string AUDIO_CLIENT_IID = "1CB9AD4C-DBFA-4C32-B178-C2F568A703B2";
    internal const string AUDIO_CLOCK_IID = "CD63314F-3FBA-4A1B-812C-EF96358728E7";
    internal const string AUDIO_CLOCK2_IID = "6F49FF73-6727-49AC-A008-D98CF5E70048";
    internal const string AUDIO_CLOCK_ADJUSTMENT_IID = "F6E4C0A0-46D9-4FB8-BE21-57A3EF2B626C";
    internal const string AUDIO_RENDER_CLIENT_IID = "F294ACFC-3146-4483-A7BF-ADDCA7C260E2";
    internal const string AUDIO_SESSION_CONTROL_IID = "F4B1A599-7266-4319-A8CA-E70ACB11E8CD";
    internal const string AUDIO_SESSION_CONTROL2_IID = "BFB7FF88-7239-4FC9-8FA2-07C950BE9C6D";
    internal const string AUDIO_SESSION_ENUMERATOR_IID = "E2F5BB11-0570-40CA-ACDD-3AA01277DEE8";
    internal const string AUDIO_SESSION_EVENTS_IID = "24918ACC-64B3-37C1-8CA9-74A66E9957A8";
    internal const string AUDIO_SESSION_MANAGER_IID = "BFA971F1-4D5E-40BB-935E-967039BFBEE4";
    internal const string AUDIO_SESSION_MANAGER2_IID = "77AA99A0-1BD6-484F-8BC7-2C654C9A9B6F";
    internal const string AUDIO_SESSION_NOTIFICATION_IID = "641DD20B-4D41-49CC-ABA3-174B9477BB08";
    internal const string AUDIO_STREAM_VOLUME_IID = "93014887-242D-4068-8A15-CF5E93B90FE3";
    internal const string AUDIO_VOLUME_DUCK_NOTIFICATION_IID = "C3B284D4-6D39-4359-B3CF-B56DDB3BB39C";
    internal const string CHANNEL_AUDIO_VOLUME_IID = "1C158861-B533-4B30-B1CF-E853E51C59B8";
    internal const string SIMPLE_AUDIO_VOLUME_IID = "87CE5498-68D6-44E5-9215-6DA47EF883D8";

    // DEVICETOPOLOGY
    internal const string AUDIO_AUTO_GAIN_CONTROL_IID = "85401FD4-6DE4-4B9D-9869-2D6753A82F3C";
    internal const string AUDIO_BASS_IID = "A2B1A1D9-4DB3-425D-A2B2-BD335CB3E2E5";
    internal const string AUDIO_CHANNEL_CONFIG_IID = "BB11C46F-EC28-493C-B88A-5DB88062CE98";
    internal const string AUDIO_INPUT_SELECTOR_IID = "4F03DC02-5E6E-4653-8F72-A030C123D598";
    internal const string AUDIO_LOUDNESS_IID = "7D8B1437-DD53-4350-9C1B-1EE2890BD938";
    internal const string AUDIO_MIDRANGE_IID = "5E54B6D7-B44B-40D9-9A9E-E691D9CE6EDF";
    internal const string AUDIO_MUTE_IID = "DF45AEEA-B74A-4B6B-AFAD-2366B6AA012E";
    internal const string AUDIO_OUTPUT_SELECTOR_IID = "BB515F69-94A7-429E-8B9C-271B3F11A3AB";
    internal const string AUDIO_PEAK_METER_IID = "DD79923C-0599-45E0-B8B6-C8DF7DB6E796";
    internal const string AUDIO_TREBLE_IID = "0A717812-694E-4907-B74B-BAFA5CFDCA7B";
    internal const string AUDIO_VOLUME_LEVEL_IID = "7FB7B48F-531D-44A2-BCB3-5AD5A134B3DC";
    internal const string CONNECTOR_IID = "9C2C4058-23F5-41DE-877A-DF3AF236A09E";
    internal const string CONTROL_CHANGE_NOTIFY_IID = "A09513ED-C709-4D21-BD7B-5F34C47F3947";
    internal const string CONTROL_INTERFACE_IID = "45D37C3F-5140-444A-AE24-400789F3CBF3";
    internal const string DEVICE_SPECIFIC_PROPERTY_IID = "3B22BCBF-2586-4AF0-8583-205D391B807C";
    internal const string DEVICE_TOPOLOGY_IID = "2A07407E-6497-4A18-9787-32F79BD0D98F";
    internal const string KS_FORMAT_SUPPORT_IID = "3CB4A69D-BB6F-4D2B-95B7-452D2C155DB5";
    internal const string KS_JACK_DESCRIPTION_IID = "4509F757-2D46-4637-8E62-CE7DB944F57B";
    internal const string KS_JACK_DESCRIPTION2_IID = "478F3A9B-E0C9-4827-9228-6F5505FFE76A";
    internal const string KS_JACK_SINK_INFORMATION_IID = "D9BD72ED-290F-4581-9FF3-61027A8FE532";
    internal const string PART_IID = "AE2DE0E4-5BCA-4F2D-AA46-5D13F8FDB3A9";
    internal const string PARTS_LIST_IID = "6DAA848C-5EB0-45CC-AEA5-998A2CDA1FFB";
    internal const string PER_CHANNEL_DB_LEVEL_IID = "C2F8E001-F205-4BC9-99BC-C13B1E048CCB";
    internal const string SUBUNIT_IID = "82149A85-DBA6-4487-86BB-EA8F7FEFCC71";

    // ENDPOINTVOLUME
    internal const string AUDIO_ENDPOINT_VOLUME_IID = "5CDF2C82-841E-4546-9722-0CF74078229A";
    internal const string AUDIO_ENDPOINT_VOLUME_EX_IID = "66E11784-F695-4F28-A505-A7080081A78F";
    internal const string AUDIO_METER_INFORMATION_IID = "C02216F6-8C67-4B5B-9D00-D008E73E0064";
    internal const string AUDIO_ENDPOINT_VOLUME_CALLBACK_IID = "657804FA-D6AD-4496-8A60-352752AF4F89";


    //POLICY CONFIG
    internal const string POLICY_CONFIG_CID = "870AF99C-171D-4F9E-AF0D-E63DF40C2BC9";
    internal const string POLICY_CONFIG_VISTA_IID = "568B9108-44BF-40B4-9006-86AFE5B5A620";
    internal const string POLICY_CONFIG_7_IID = "F8679F50-850A-41CF-9C72-430F290290C8";
    internal const string POLICY_CONFIG_X_IID = "CA286FC3-91FD-42C3-8E9B-CAAFA66242E3"; //Pre Redstone 1 patch
    internal const string POLICY_CONFIG_X_RS_IID = "00632A31-4D49-4167-8AE1-27F82CE135B1"; //Post Redstone 1
    internal const string POLICY_CONFIG_X_RS2_IID = "098FF37B-1062-4B1A-AD73-2A2D530FEAB6"; //Post Redstone 2
    internal const string POLICY_CONFIG_X_RS3_IID = "5731289F-3E89-4209-86BE-6599A8E05E67";
}