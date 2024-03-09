using System;
using System.Runtime.InteropServices;

namespace AudioSwitcher.AudioApi.CoreAudio.Interfaces;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 2)]
internal class WaveFormatExtensible : WaveFormatEx
{
    private readonly int dwChannelMask;

    /// <summary>
    /// Parameterless constructor for marshalling
    /// </summary>
    // ReSharper disable once UnusedMember.Local
    private WaveFormatExtensible()
    {
    }

    /// <summary>
    /// Creates a new WaveFormatExtensible for PCM
    /// KSDATAFORMAT_SUBTYPE_PCM
    /// </summary>
    public WaveFormatExtensible(SampleRate rate, BitDepth bits, SpeakerConfiguration channelMask)
        : this(rate, bits, channelMask, new Guid("00000001-0000-0010-8000-00AA00389B71"))
    {
        ValidBitsPerSample = (short)bits;
        dwChannelMask = (int)channelMask;
    }

    public WaveFormatExtensible(SampleRate rate, BitDepth bits, SpeakerConfiguration channelMask, Guid subFormat)
        : base(rate, bits, channelMask, WaveFormatEncoding.Extensible, Marshal.SizeOf(typeof(WaveFormatExtensible)))
    {
        ValidBitsPerSample = (short)bits;
        dwChannelMask = (int)channelMask;

        SubFormat = subFormat;
    }

    public short ValidBitsPerSample { get; }

    public SpeakerConfiguration ChannelMask => (SpeakerConfiguration)dwChannelMask;

    public Guid SubFormat { get; }

    public override string ToString()
    {
        return
            $"{base.ToString()} wBitsPerSample:{ValidBitsPerSample} dwChannelMask:{dwChannelMask} subFormat:{SubFormat} extraSize:{ExtraSize}";
    }
}