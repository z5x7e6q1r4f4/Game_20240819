using System;

namespace Main.RXs
{
    partial class RXsObserver
    {
        private class ObserverFromAction<T> : RXsObserverBaseReusable<ObserverFromAction<T>, T>
        {
            private Action<T> onNext;
            private Action onCompleted;
            private Action<Exception> onError;
            private Action onRelease;
            protected override void OnNext(T value) => onNext?.Invoke(value);
            protected override void OnCompleted() => onCompleted?.Invoke();
            protected override void OnError(Exception error) => onError?.Invoke(error);
            protected override void OnRelease()
            {
                base.OnRelease();
                onRelease?.Invoke();
                onRelease = null;
                onNext = null;
                onCompleted = null;
                onError = null;
            }
            public static ObserverFromAction<T> GetFromReusePool(
                Action<T> onNext,
                Action onCompleted,
                Action<Exception> onError,
                Action onRelease)
            {
                var observer = GetFromReusePool();
                observer.onNext = onNext;
                observer.onCompleted = onCompleted;
                observer.onError = onError;
                observer.onRelease = onRelease;
                return observer;
            }
            private ObserverFromAction() { }
        }
        //FromAction
        public static IRXsObserver<T> FromAction<T>(Action<T> onNext = null, Action onCompleted = null, Action<Exception> onError = null, Action onRelease = null)
            => ObserverFromAction<T>.GetFromReusePool(onNext, onCompleted, onError, onRelease);
        public static IRXsObserver FromAction(Action<object> onNext = null, Action onCompleted = null, Action<Exception> onError = null, Action onRelease = null)
            => FromAction<object>(onNext, onCompleted, onError, onRelease);
        //Typed
        public static IRXsSubscription Subscribe<T>(this IRXsObservable<T> observable, Action<T> onNext = null, Action onCompleted = null, Action<Exception> onError = null, Action onRelease = null)
            => observable.SubscribeToTyped(FromAction(onNext, onCompleted, onError, onRelease));
        public static IRXsSubscription Subscribe<T>(this IRXsObservable<T> observable, Action onNext = null, Action onCompleted = null, Action<Exception> onError = null, Action onRelease = null)
            => observable.Subscribe(onNext != null ? (_) => onNext() : null, onCompleted, onError, onRelease);
        //Untyped
        public static IRXsSubscription Subscribe(this IRXsObservable observable, Action<object> onNext = null, Action onCompleted = null, Action<Exception> onError = null, Action onRelease = null)
         => observable.Subscribe(FromAction(onNext, onCompleted, onError, onRelease));
        public static IRXsSubscription Subscribe(this IRXsObservable observable, Action onNext = null, Action onCompleted = null, Action<Exception> onError = null, Action onRelease = null)
            => observable.Subscribe(onNext != null ? (_) => onNext() : null, onCompleted, onError, onRelease);
    }
}