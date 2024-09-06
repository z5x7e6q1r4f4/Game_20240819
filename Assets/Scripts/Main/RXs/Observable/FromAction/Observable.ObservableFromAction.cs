using System;

namespace Main.RXs
{
    public static partial class Observable
    {
        private sealed class ObservableFromAction<T> :
            Reuse.ObjectBase<ObservableFromAction<T>>,
            IObservableFromAction<T>,
            IReuseable.IOnRelease
        {
            public event Func<IObservableFromAction<T>, IObserverDisposableHandler<T>, IDisposable> OnSubscribeFunction;
            public event Action<IObservableFromAction<T>> OnDisposeAction;
            public void Dispose() => this.ReleaseToReusePool();
            void IReuseable.IOnRelease.OnRelease()
            {
                OnSubscribeFunction = null;
                OnDisposeAction?.Invoke(this);
                OnDisposeAction = null;
            }
            public IDisposable Subscribe(IObserver<T> observer) => OnSubscribeFunction?.Invoke(this, observer.ToDisposableHandler());
            public static ObservableFromAction<T> GetFromReusePool() => GetFromReusePool(false);
        }
    }
}
