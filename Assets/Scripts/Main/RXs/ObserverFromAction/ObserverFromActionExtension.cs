using System;

namespace Main.RXs
{
    public static class ObserverFromActionExtension
    {
        private class ObserverFromAction<T> : ObserverNode<T>
        {
            private Action<T> onNext;
            private Action onCompleted;
            private Action<Exception> onError;
            public override void OnNext(T value)
            {
                onNext?.Invoke(value);
                base.OnNext(value);
            }
            public override void OnCompleted()
            {
                onCompleted?.Invoke();
                base.OnCompleted();
            }
            public override void OnError(Exception error)
            {
                onError?.Invoke(error);
                base.OnError(error);
            }
            public override void Dispose()
            {
                onNext = null;
                onCompleted = null;
                onError = null;
                base.Dispose();
            }
            public ObserverFromAction(Action<T> onNext = null, Action onCompleted = null, Action<Exception> onError = null)
            {
                this.onNext = onNext;
                this.onCompleted = onCompleted;
                this.onError = onError;
            }
        }
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext = null, Action onCompleted = null, Action<Exception> onError = null)
            => observable.Subscribe(new ObserverFromAction<T>(onNext, onCompleted, onError));
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action onNext = null, Action onCompleted = null, Action<Exception> onError = null)
            => observable.Subscribe(new ObserverFromAction<T>(onNext != null ? (_) => onNext() : null, onCompleted, onError));
    }
}