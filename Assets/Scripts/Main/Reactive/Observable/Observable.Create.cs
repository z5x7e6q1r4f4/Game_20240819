using System;

namespace Main
{
    public static partial class Observable
    {
        public static ObservableFromAction<T> Create<T>(
            Func<ObservableFromAction<T>, IObserverDisposableHandler<T>, IDisposable> onSubscribe = null,
            Action<ObservableFromAction<T>> onDispose = null)
        {
            var observable = ObservableFromAction<T>.GetFromReusePool();
            observable.OnSubscribeFunction += onSubscribe;
            observable.OnDisposeAction += onDispose;
            return observable;
        }
        public static ObservableFromAction<T> Create<T>(
         Func<IObserverDisposableHandler<T>, IDisposable> onSubscribe = null,
         Action onDispose = null)
            => Create<T>(
                onSubscribe != null ? (_, observer) => onSubscribe(observer) : null,
                onDispose != null ? _ => onDispose() : null);
        public static ObservableFromAction<T> Create<T>()
            => Create(default(Func<ObservableFromAction<T>, IObserverDisposableHandler<T>, IDisposable>));
    }
}
