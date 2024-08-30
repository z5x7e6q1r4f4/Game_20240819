using System;

namespace Main.RXs
{
    partial class RXsObserver
    {
        private class ObserverFromAction<T> : RXsObserverItemReusable<ObserverFromAction<T>, T>
        {
            private Action<T> onNext;
            private Action onCompleted;
            private Action<Exception> onError;
            protected override void OnNext(T value) => onNext?.Invoke(value);
            protected override void OnCompleted() => onCompleted?.Invoke();
            protected override void OnError(Exception error) => onError?.Invoke(error);
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
        public static IObserver<T> FromAction<T>(Action<T> onNext = null, Action onCompleted = null, Action<Exception> onError = null)
            => ObserverFromAction<T>.GetFromReusePool(onNext, onCompleted, onError);
        public static IDisposable Subscribe<T>(this IRXsObservable<T> observable, Action<T> onNext = null, Action onCompleted = null, Action<Exception> onError = null)
            => observable.SubscribeToTyped(FromAction(onNext, onCompleted, onError));
        public static IDisposable Subscribe<T>(this IRXsObservable<T> observable, Action onNext = null, Action onCompleted = null, Action<Exception> onError = null)
            => observable.SubscribeToTyped(FromAction<T>(onNext != null ? (_) => onNext() : null, onCompleted, onError));
    }
}