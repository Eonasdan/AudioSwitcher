/*
  LICENSE
  -------
  Copyright (C) 2007 Ray Molenkamp

  This source code is provided 'as-is', without any express or implied
  warranty.  In no event will the authors be held liable for any damages
  arising from the use of this source code or the software it produces.

  Permission is granted to anyone to use this source code for any purpose,
  including commercial applications, and to alter it and redistribute it
  freely, subject to the following restrictions:

  1. The origin of this source code must not be misrepresented; you must not
     claim that you wrote the original source code.  If you use this source code
     in a product, an acknowledgment in the product documentation would be
     appreciated but is not required.
  2. Altered source versions must be plainly marked as such, and must not be
     misrepresented as being the original source code.
  3. This notice may not be removed or altered from any source distribution.
*/

using System;
using System.Runtime.InteropServices;
using AudioSwitcher.AudioApi.CoreAudio.Interfaces;
using AudioSwitcher.AudioApi.CoreAudio.Threading;

namespace AudioSwitcher.AudioApi.CoreAudio;

internal delegate void AudioEndpointVolumeNotificationDelegate(AudioVolumeNotificationData data);

internal class AudioEndpointVolume : IDisposable
{
    private IAudioEndpointVolume _audioEndPointVolume;
    private AudioEndpointVolumeCallback _callBack;

    /// <summary>
    /// Creates a new Audio endpoint volume
    /// </summary>
    /// <param name="realEndpointVolume">IAudioEndpointVolume COM interface</param>
    internal AudioEndpointVolume(IAudioEndpointVolume realEndpointVolume)
    {
        ComThread.Assert();

        _audioEndPointVolume = realEndpointVolume;
        Channels = new AudioEndpointVolumeChannels(_audioEndPointVolume);
        StepInformation = new AudioEndpointVolumeStepInformation(_audioEndPointVolume);
        Marshal.ThrowExceptionForHR(_audioEndPointVolume.QueryHardwareSupport(out var hardwareSupp));
        HardwareSupport = (EndpointHardwareSupport)hardwareSupp;
        VolumeRange = new AudioEndpointVolumeVolumeRange(_audioEndPointVolume);

        _callBack = new AudioEndpointVolumeCallback(this);
        Marshal.ThrowExceptionForHR(_audioEndPointVolume.RegisterControlChangeNotify(_callBack));
    }

    /// <summary>
    /// VolumeChanged Range
    /// </summary>
    public AudioEndpointVolumeVolumeRange VolumeRange { get; }

    /// <summary>
    /// Hardware Support
    /// </summary>
    public EndpointHardwareSupport HardwareSupport { get; }

    /// <summary>
    /// Step Information
    /// </summary>
    public AudioEndpointVolumeStepInformation StepInformation { get; }

    /// <summary>
    /// Channels
    /// </summary>
    public AudioEndpointVolumeChannels Channels { get; }

    /// <summary>
    /// Master VolumeChanged Level
    /// </summary>
    public float MasterVolumeLevel
    {
        get
        {
            return ComThread.Invoke(() =>
            {
                Marshal.ThrowExceptionForHR(_audioEndPointVolume.GetMasterVolumeLevel(out var result));
                return result;
            });
        }
        set
        {
            ComThread.Invoke(() =>
            {
                Marshal.ThrowExceptionForHR(_audioEndPointVolume.SetMasterVolumeLevel(value, Guid.Empty));
            });
        }
    }

    /// <summary>
    /// Master VolumeChanged Level Scalar
    /// </summary>
    public float MasterVolumeLevelScalar
    {
        get
        {
            return ComThread.Invoke(() =>
            {
                Marshal.ThrowExceptionForHR(_audioEndPointVolume.GetMasterVolumeLevelScalar(out var result));
                return result;
            });
        }
        set
        {
            ComThread.BeginInvoke(() =>
            {
                Marshal.ThrowExceptionForHR(_audioEndPointVolume.SetMasterVolumeLevelScalar(value, Guid.Empty));
            });
        }
    }

    /// <summary>
    /// Mute
    /// </summary>
    public bool Mute
    {
        get
        {
            return ComThread.Invoke(() =>
            {
                Marshal.ThrowExceptionForHR(_audioEndPointVolume.GetMute(out var result));
                return result;
            });
        }
        set
        {
            ComThread.BeginInvoke(() => Marshal.ThrowExceptionForHR(_audioEndPointVolume.SetMute(value, Guid.Empty)));
        }
    }

    /// <summary>
    /// Dispose
    /// </summary>
    public void Dispose()
    {
        if (_callBack != null)
            ComThread.BeginInvoke(() =>
            {
                _audioEndPointVolume?.UnregisterControlChangeNotify(_callBack);

                _callBack = null;
                _audioEndPointVolume = null;
            });

        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// On VolumeChanged Notification
    /// </summary>
    public event AudioEndpointVolumeNotificationDelegate OnVolumeNotification;

    /// <summary>
    /// VolumeChanged Step Down
    /// </summary>
    public void VolumeStepDown()
    {
        ComThread.Invoke(() => Marshal.ThrowExceptionForHR(_audioEndPointVolume.VolumeStepDown(Guid.Empty)));
    }

    /// <summary>
    /// VolumeChanged Step Up
    /// </summary>
    public void VolumeStepUp()
    {
        ComThread.Invoke(() => Marshal.ThrowExceptionForHR(_audioEndPointVolume.VolumeStepUp(Guid.Empty)));
    }

    internal void FireNotification(AudioVolumeNotificationData notificationData)
    {
        OnVolumeNotification?.Invoke(notificationData);
    }

    /// <summary>
    /// Finalizer
    /// </summary>
    ~AudioEndpointVolume()
    {
        Dispose();
    }
}