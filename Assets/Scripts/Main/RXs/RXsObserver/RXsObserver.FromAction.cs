using System;

namespace Main.RXs
{
    partial class RXsObserver
    {
        private class ObserverFromAction<T> :
            RXsObserverBaseReusable<ObserverFromAction<T>, T>
        {
            private Action<IRXsDisposable, T> onNext;
            private Action<IRXsDisposable> onCompleted;
            private Action<IRXsDisposable, Exception> onError;
            private Action onDispose;
            protected override void OnNext(T value) => onNext?.Invoke(this, value);
            protected override void OnCompleted() => onCompleted?.Invoke(this);
            protected override void OnError(Exception error) => onError?.Invoke(this, error);
            protected override void Dispose()
            {
                onDispose?.Invoke();
                onDispose = null;
                onNext = null;
                onCompleted = null;
                onError = null;
                base.Dispose();
            }
            public static ObserverFromAction<T> GetFromReusePool(
                Action<IRXsDisposable, T> onNext,
                Action<IRXsDisposable> onCompleted,
                Action<IRXsDisposable, Exception> onError,
                Action onDispose)
            {
                var observer = GetFromReusePool();
                observer.onNext = onNext;
                observer.onCompleted = onCompleted;
                observer.onError = onError;
                observer.onDispose = onDispose;
                return observer;
            }
            private ObserverFromAction() { }
        }
        //FromActionTyped
        public static IRXsObserverSubscription<T> FromAction<T>(
            Action<IRXsDisposable, T> onNext = null,
            Action<IRXsDisposable> onCompleted = null,
            Action<IRXsDisposable, Exception> onError = null,
            Action onDispose = null)
            => ObserverFromAction<T>.GetFromReusePool(onNext, onCompleted, onError, onDispose);
        public static IRXsObserverSubscription<T> FromAction<T>(
            Action<T> onNext = null,
            Action onCompleted = null,
            Action<Exception> onError = null,
            Action onDispose = null)
            => FromAction<T>(
                onNext != null ? (_, x) => onNext(x) : null,
                onCompleted != null ? _ => onCompleted() : null,
                onError != null ? (_, e) => onError(e) : null,
                onDispose);
        public static IRXsObserverSubscription<T> FromAction<T>(
            Action onNext = null,
            Action onCompleted = null,
            Action onError = null,
            Action onDispose = null)
            => FromAction<T>(
                onNext != null ? _ => onNext() : null,
                onCompleted,
                onError != null ? _ => onError() : null,
                onDispose);
        //FromActionUntyped
        public static IRXsObserverSubscription FromAction(
            Action<IRXsDisposable, object> onNext = null,
            Action<IRXsDisposable> onCompleted = null,
            Action<IRXsDisposable, Exception> onError = null,
            Action onDispose = null)
            => ObserverFromAction<object>.GetFromReusePool(onNext, onCompleted, onError, onDispose);
        public static IRXsObserverSubscription FromAction(
            Action<object> onNext = null,
            Action onCompleted = null,
            Action<Exception> onError = null,
            Action onDispose = null)
            => FromAction<object>(
                onNext != null ? (_, x) => onNext(x) : null,
                onCompleted != null ? _ => onCompleted() : null,
                onError != null ? (_, e) => onError(e) : null,
                onDispose);
        public static IRXsObserverSubscription FromAction(
            Action onNext = null,
            Action onCompleted = null,
            Action onError = null,
            Action onDispose = null)
            => FromAction<object>(
                onNext != null ? _ => onNext() : null,
                onCompleted,
                onError != null ? _ => onError() : null,
                onDispose);
        //SubscribeTyped
        public static IRXsDisposable Subscribe<T>(
            this IRXsObservable<T> observable,
            Action<IRXsDisposable, T> onNext = null,
            Action<IRXsDisposable> onCompleted = null,
            Action<IRXsDisposable, Exception> onError = null,
            Action onDispose = null)
            => observable.SubscribeToTyped(FromAction(onNext, onCompleted, onError, onDispose));
        public static IRXsDisposable Subscribe<T>(
            this IRXsObservable<T> observable,
            Action<T> onNext = null,
            Action onCompleted = null,
            Action<Exception> onError = null,
            Action onDispose = null)
            => observable.SubscribeToTyped(FromAction(onNext, onCompleted, onError, onDispose));
        public static IRXsDisposable Subscribe<T>(
            this IRXsObservable<T> observable,
            Action onNext = null,
            Action onCompleted = null,
            Action onError = null,
            Action onDispose = null)
            => observable.SubscribeToTyped(FromAction<T>(onNext, onCompleted, onError, onDispose));
        //SubscribeUntyped
        public static IRXsDisposable Subscribe(
            this IRXsObservable observable,
            Action<IRXsDisposable, object> onNext = null,
            Action<IRXsDisposable> onCompleted = null,
            Action<IRXsDisposable, Exception> onError = null,
            Action onDispose = null)
            => observable.Subscribe(FromAction(onNext, onCompleted, onError, onDispose));
        public static IRXsDisposable Subscribe(
            this IRXsObservable observable,
            Action<object> onNext = null,
            Action onCompleted = null,
            Action<Exception> onError = null,
            Action onDispose = null)
            => observable.Subscribe(FromAction(onNext, onCompleted, onError, onDispose));
        public static IRXsDisposable Subscribe(
            this IRXsObservable observable,
            Action onNext = null,
            Action onCompleted = null,
            Action onError = null,
            Action onDispose = null)
            => observable.Subscribe(FromAction(onNext, onCompleted, onError, onDispose));
    }
}