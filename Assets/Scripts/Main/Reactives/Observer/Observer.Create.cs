using System;

namespace Main
{
    public static partial class Observer
    {
        public static ObserverFromAction<T> Create<T>(
            Action<ObserverFromAction<T>, T> onNext = null,
            Action<ObserverFromAction<T>> onCompleted = null,
            Action<ObserverFromAction<T>, Exception> onError = null,
            Action<IDisposableBase> onDispose = null)
        {
            var observer = ObserverFromAction<T>.GetFromReusePool();
            observer.OnNextAction += onNext;
            observer.OnCompletedAction += onCompleted;
            observer.OnErrorAction += onError;
            observer.OnDisposeAction += onDispose;
            return observer;
        }
        public static ObserverFromAction<T> Create<T>(
            Action<T> onNext = null,
            Action onCompleted = null,
            Action<Exception> onError = null,
            Action onDispose = null)
        => Create<T>(
            onNext != null ? (_, value) => onNext(value) : null,
            onCompleted != null ? _ => onCompleted() : null,
            onError != null ? (_, error) => onError(error) : null,
            onDispose != null ? _ => onDispose() : null);
        public static ObserverFromAction<T> Create<T>(
            Action onNext = null,
            Action onCompleted = null,
            Action onError = null,
            Action onDispose = null)
        => Create<T>(
            onNext != null ? _ => onNext() : null,
            onCompleted,
            onError != null ? _ => onError() : null,
            onDispose);
        public static ObserverFromAction<T> Create<T>()
            => Create(default(Action<ObserverFromAction<T>, T>));
    }
}