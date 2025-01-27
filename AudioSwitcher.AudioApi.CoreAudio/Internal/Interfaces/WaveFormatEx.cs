using System;
using System.Runtime.InteropServices;

namespace AudioSwitcher.AudioApi.CoreAudio.Interfaces;

[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Ansi, Pack = 2)]
internal class WaveFormatEx
{
    private readonly short bitsPerSample;
    private readonly short blockAlign;
    private readonly short channels;
    private readonly short extraSize;

    protected WaveFormatEx()
    {
    }

    public WaveFormatEx(SampleRate rate, BitDepth bits, SpeakerConfiguration channelMask)
        : this(rate, bits, channelMask, WaveFormatEncoding.Pcm, Marshal.SizeOf(typeof(WaveFormatEx)))
    {
    }

    protected WaveFormatEx(SampleRate rate, BitDepth bits, SpeakerConfiguration channelMask,
        WaveFormatEncoding formatTag, int totalSize)
    {
        channels = ChannelsFromMask((int)channelMask);

        if (channels < 1) throw new ArgumentOutOfRangeException(nameof(channelMask), "Channels must be 1 or greater");

        SampleRate = (int)rate;
        bitsPerSample = (short)bits;
        extraSize = 0;

        blockAlign = (short)(channels * (bitsPerSample / 8));
        AverageBytesPerSecond = SampleRate * blockAlign;

        Encoding = formatTag;
        extraSize = (short)(totalSize - Marshal.SizeOf(typeof(WaveFormatEx)));
    }

    public WaveFormatEncoding Encoding { get; }

    public int Channels => channels;

    public int SampleRate { get; }

    public int AverageBytesPerSecond { get; }

    public virtual int BlockAlign => blockAlign;

    public int BitsPerSample => bitsPerSample;

    public int ExtraSize => extraSize;

    public override string ToString()
    {
        switch (Encoding)
        {
            case WaveFormatEncoding.Pcm:
            case WaveFormatEncoding.Extensible:
                // formatTag just has some extra bits after the PCM header
                return $"{bitsPerSample} bit PCM: {SampleRate / 1000}kHz {channels} channels";
            default:
                return Encoding.ToString();
        }
    }

    private short ChannelsFromMask(int channelMask)
    {
        short count = 0;

        // until all bits are zero
        while (channelMask > 0)
        {
            // check lower bit
            if ((channelMask & 1) == 1)
                count++;

            // shift bits, removing lower bit
            channelMask >>= 1;
        }

        return count;
    }
}