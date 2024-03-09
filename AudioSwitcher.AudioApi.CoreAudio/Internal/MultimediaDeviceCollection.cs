using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using AudioSwitcher.AudioApi.CoreAudio.Interfaces;
using AudioSwitcher.AudioApi.CoreAudio.Threading;

namespace AudioSwitcher.AudioApi.CoreAudio;

internal class MultimediaDeviceCollection : IEnumerable<IMultimediaDevice>, IDisposable
{
    private IMultimediaDeviceCollection _multimediaDeviceCollection;

    internal MultimediaDeviceCollection(IMultimediaDeviceCollection parent)
    {
        ComThread.Assert();
        _multimediaDeviceCollection = parent;
    }

    /// <summary>
    /// Device count
    /// </summary>
    public int Count
    {
        get
        {
            ComThread.Assert();
            Marshal.ThrowExceptionForHR(_multimediaDeviceCollection.GetCount(out var result));
            return Convert.ToInt32(result);
        }
    }

    /// <summary>
    /// Get device by index
    /// </summary>
    /// <param name="index">Device index</param>
    /// <returns>Device at the specified index</returns>
    public IMultimediaDevice this[int index]
    {
        get
        {
            ComThread.Assert();
            _multimediaDeviceCollection.Item(Convert.ToUInt32(index), out var result);
            return result;
        }
    }

    public void Dispose()
    {
        Dispose(true);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public IEnumerator<IMultimediaDevice> GetEnumerator()
    {
        for (var index = 0; index < Count; index++) yield return this[index];
    }

    protected void Dispose(bool disposing)
    {
        _multimediaDeviceCollection = null;
    }

    ~MultimediaDeviceCollection()
    {
        Dispose(false);
    }
}