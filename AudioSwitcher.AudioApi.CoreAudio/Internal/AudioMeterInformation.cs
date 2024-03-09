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

/// <summary>
/// Audio Meter Information
/// </summary>
internal class AudioMeterInformation : IDisposable
{
    private IAudioMeterInformation _audioMeterInformation;

    internal AudioMeterInformation(IAudioMeterInformation realInterface)
    {
        ComThread.Assert();

        _audioMeterInformation = realInterface;
        Marshal.ThrowExceptionForHR(_audioMeterInformation.QueryHardwareSupport(out var hardwareSupp));
        HardwareSupport = (EndpointHardwareSupport)hardwareSupp;
        PeakValues = new AudioMeterInformationChannels(_audioMeterInformation);
    }

    /// <summary>
    /// Peak Values
    /// </summary>
    public AudioMeterInformationChannels PeakValues { get; private set; }

    /// <summary>
    /// Hardware Support
    /// </summary>
    public EndpointHardwareSupport HardwareSupport { get; }

    /// <summary>
    /// Master Peak Value
    /// </summary>
    public float MasterPeakValue
    {
        get
        {
            return ComThread.Invoke(() =>
            {
                Marshal.ThrowExceptionForHR(_audioMeterInformation.GetPeakValue(out var result));
                return result;
            });
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }

    private void Dispose(bool disposing)
    {
        if (disposing)
        {
            _audioMeterInformation = null;
            PeakValues = null;
        }
    }

    ~AudioMeterInformation()
    {
        Dispose(false);
    }
}