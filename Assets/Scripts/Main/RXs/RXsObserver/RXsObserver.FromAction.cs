using System;

namespace Main.RXs
{
    partial class RXsObserver
    {
        private class ObserverFromAction<T> :
            RXsObserverBaseReusable<ObserverFromAction<T>, T>
        {
            private Action<IRXsSubscription, T> onNext;
            private Action<IRXsSubscription> onCompleted;
            private Action<IRXsSubscription, Exception> onError;
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
                Action<IRXsSubscription, T> onNext,
                Action<IRXsSubscription> onCompleted,
                Action<IRXsSubscription, Exception> onError,
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
            Action<IRXsSubscription, T> onNext = null,
            Action<IRXsSubscription> onCompleted = null,
            Action<IRXsSubscription, Exception> onError = null,
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
            Action<IRXsSubscription, object> onNext = null,
            Action<IRXsSubscription> onCompleted = null,
            Action<IRXsSubscription, Exception> onError = null,
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
        public static IRXsSubscription Subscribe<T>(
            this IRXsObservable<T> observable,
            Action<IRXsSubscription, T> onNext = null,
            Action<IRXsSubscription> onCompleted = null,
            Action<IRXsSubscription, Exception> onError = null,
            Action onDispose = null)
            => observable.SubscribeToTyped(FromAction(onNext, onCompleted, onError, onDispose));
        public static IRXsSubscription Subscribe<T>(
            this IRXsObservable<T> observable,
            Action<T> onNext = null,
            Action onCompleted = null,
            Action<Exception> onError = null,
            Action onDispose = null)
            => observable.SubscribeToTyped(FromAction(onNext, onCompleted, onError, onDispose));
        public static IRXsSubscription Subscribe<T>(
            this IRXsObservable<T> observable,
            Action onNext = null,
            Action onCompleted = null,
            Action onError = null,
            Action onDispose = null)
            => observable.SubscribeToTyped(FromAction<T>(onNext, onCompleted, onError, onDispose));
        //SubscribeUntyped
        public static IRXsSubscription Subscribe(
            this IRXsObservable observable,
            Action<IRXsSubscription, object> onNext = null,
            Action<IRXsSubscription> onCompleted = null,
            Action<IRXsSubscription, Exception> onError = null,
            Action onDispose = null)
            => observable.Subscribe(FromAction(onNext, onCompleted, onError, onDispose));
        public static IRXsSubscription Subscribe(
            this IRXsObservable observable,
            Action<object> onNext = null,
            Action onCompleted = null,
            Action<Exception> onError = null,
            Action onDispose = null)
            => observable.Subscribe(FromAction(onNext, onCompleted, onError, onDispose));
        public static IRXsSubscription Subscribe(
            this IRXsObservable observable,
            Action onNext = null,
            Action onCompleted = null,
            Action onError = null,
            Action onDispose = null)
            => observable.Subscribe(FromAction(onNext, onCompleted, onError, onDispose));
    }
}