using System;

namespace Main.RXs
{
    partial class Observer
    {
        private class ObserverFromAction<T> : ObserverNodeReusable<T, ObserverFromAction<T>>
        {
            private Action<T> onNext;
            private Action onCompleted;
            private Action<Exception> onError;
            protected override void OnNext(T value)
            {
                onNext?.Invoke(value);
                base.OnNext(value);
            }
            protected override void OnCompleted()
            {
                onCompleted?.Invoke();
                base.OnCompleted();
            }
            protected override void OnError(Exception error)
            {
                onError?.Invoke(error);
                base.OnError(error);
            }
            protected override void OnRelease()
            {
                onNext = null;
                onCompleted = null;
                onError = null;
                base.OnRelease();
            }
            public static ObserverFromAction<T> GetFromReusePool(
                Action<T> onNext,
                Action onCompleted,
                Action<Exception> onError)
            {
                var observer = GetFromReusePool();
                observer.onNext = onNext;
                observer.onCompleted = onCompleted;
                observer.onError = onError;
                return observer;
            }
        }
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action<T> onNext = null, Action onCompleted = null, Action<Exception> onError = null)
            => observable.SubscribeToTyped<T>(ObserverFromAction<T>.GetFromReusePool(onNext, onCompleted, onError));
        public static IDisposable Subscribe<T>(this IObservable<T> observable, Action onNext = null, Action onCompleted = null, Action<Exception> onError = null)
            => observable.SubscribeToTyped<T>(ObserverFromAction<T>.GetFromReusePool(onNext != null ? (_) => onNext() : null, onCompleted, onError));
    }
}