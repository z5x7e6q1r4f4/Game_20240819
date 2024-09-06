using System;

namespace Main.RXs
{
    public static partial class Observable
    {
        public static IObservableFromAction<T> Create<T>(
            Func<IObservableFromAction<T>, IObserverDisposableHandler<T>, IDisposable> onSubscribe = null,
            Action<IObservableFromAction<T>> onDispose = null)
        {
            var observable = ObservableFromAction<T>.GetFromReusePool();
            observable.OnSubscribeFunction += onSubscribe;
            observable.OnDisposeAction += onDispose;
            return observable;
        }
        public static IObservableFromAction<T> Create<T>(
         Func<IObserverDisposableHandler<T>, IDisposable> onSubscribe = null,
         Action onDispose = null)
            => Create<T>(
                onSubscribe != null ? (_, observer) => onSubscribe(observer) : null,
                onDispose != null ? _ => onDispose() : null);
        public static IObservableFromAction<T> Create<T>()
            => Create(default(Func<IObservableFromAction<T>, IObserverDisposableHandler<T>, IDisposable>));
    }
}
