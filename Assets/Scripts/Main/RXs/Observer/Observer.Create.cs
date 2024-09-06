using System;

namespace Main.RXs
{
    public static partial class Observer
    {
        public static IObserverFromAction<T> Create<T>(
            Action<IObserverFromAction<T>, T> onNext = null,
            Action<IObserverFromAction<T>> onCompleted = null,
            Action<IObserverFromAction<T>, Exception> onError = null,
            Action<IObserverFromAction<T>> onDispose = null)
        {
            var observer = ObserverFromAction<T>.GetFromReusePool();
            observer.OnNextAction += onNext;
            observer.OnCompletedAction += onCompleted;
            observer.OnErrorAction += onError;
            observer.OnDisposeAction += onDispose;
            return observer;
        }
        public static IObserverFromAction<T> Create<T>(
            Action<T> onNext = null,
            Action onCompleted = null,
            Action<Exception> onError = null,
            Action onDispose = null)
        => Create<T>(
            onNext != null ? (_, value) => onNext(value) : null,
            onCompleted != null ? _ => onCompleted() : null,
            onError != null ? (_, error) => onError(error) : null,
            onDispose != null ? _ => onDispose() : null);
        public static IObserverFromAction<T> Create<T>(
            Action onNext = null,
            Action onCompleted = null,
            Action onError = null,
            Action onDispose = null)
        => Create<T>(
            onNext != null ? _ => onNext() : null,
            onCompleted,
            onError != null ? _ => onError() : null,
            onDispose);
        public static IObserverFromAction<T> Create<T>()
            => Create(default(Action<IObserverFromAction<T>, T>));
    }
}