﻿using System;
using System.Runtime.InteropServices;

namespace AudioSwitcher.AudioApi.CoreAudio.Interfaces;

[Guid(ComInterfaceIds.AUDIO_SESSION_CONTROL_IID)]
[InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
internal interface IAudioSessionControl
{
    [PreserveSig]
    int GetState([Out] out EAudioSessionState state);

    [PreserveSig]
    int GetDisplayName([Out] [MarshalAs(UnmanagedType.LPWStr)] out string displayName);

    [PreserveSig]
    int SetDisplayName(
        [In] [MarshalAs(UnmanagedType.LPWStr)] string displayName,
        [In] [MarshalAs(UnmanagedType.LPStruct)]
        Guid eventContext);

    [PreserveSig]
    int GetIconPath([Out] [MarshalAs(UnmanagedType.LPWStr)] out string iconPath);

    [PreserveSig]
    int SetIconPath(
        [In] [MarshalAs(UnmanagedType.LPWStr)] string iconPath,
        [In] [MarshalAs(UnmanagedType.LPStruct)]
        Guid eventContext);

    [PreserveSig]
    int GetGroupingParam([Out] out Guid groupingId);

    [PreserveSig]
    int SetGroupingParam(
        [In] [MarshalAs(UnmanagedType.LPStruct)]
        Guid groupingId,
        [In] [MarshalAs(UnmanagedType.LPStruct)]
        Guid eventContext);

    [PreserveSig]
    int RegisterAudioSessionNotification([In] IAudioSessionEvents client);

    [PreserveSig]
    int UnregisterAudioSessionNotification([In] IAudioSessionEvents client);
}