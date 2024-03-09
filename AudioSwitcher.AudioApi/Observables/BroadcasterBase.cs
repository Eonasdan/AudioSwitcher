using System;

namespace AudioSwitcher.AudioApi.Observables;

public abstract class BroadcasterBase<T> : IBroadcaster<T>, IDisposable
{
    public abstract bool HasObservers { get; }

    public bool IsDisposed { get; private set; }

    public abstract bool IsComplete { get; }

    public void Dispose()
    {
        if (IsDisposed)
            return;

        Dispose(true);
        GC.SuppressFinalize(this);
        IsDisposed = true;
    }

    public abstract IDisposable Subscribe(IObserver<T> observer);

    public abstract void OnCompleted();

    public abstract void OnError(Exception error);

    public abstract void OnNext(T value);

    protected abstract void Dispose(bool disposing);
}