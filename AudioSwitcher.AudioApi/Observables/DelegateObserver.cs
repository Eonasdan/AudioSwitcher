using System;
using System.Threading;

namespace AudioSwitcher.AudioApi.Observables;

internal sealed class DelegateObserver<T> : IObserver<T>, IDisposable
{
    private readonly Action _onCompleted;
    private readonly Action<Exception> _onError;
    private readonly Action<T> _onNext;

    private int _isStopped;

    public DelegateObserver(Action<T> onNext, Action<Exception> onError, Action onCompleted)
    {
        if (onNext == null) throw new ArgumentNullException(nameof(onNext));
        if (onError == null) throw new ArgumentNullException(nameof(onError));
        if (onCompleted == null) throw new ArgumentNullException(nameof(onCompleted));

        _onNext = onNext;
        _onError = onError;
        _onCompleted = onCompleted;
    }

    internal bool IsDisposed => _isStopped == 1;

    public void Dispose()
    {
        Interlocked.Exchange(ref _isStopped, 1);
    }

    public void OnCompleted()
    {
        if (Interlocked.Exchange(ref _isStopped, 1) == 0)
            _onCompleted();
    }

    public void OnError(Exception error)
    {
        if (Interlocked.Exchange(ref _isStopped, 1) == 0)
            _onError(error);
    }

    public void OnNext(T value)
    {
        if (_isStopped == 0)
            _onNext(value);
    }
}