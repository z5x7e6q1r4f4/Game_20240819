using System;

namespace Main
{
    partial class Observable
    {
        public sealed class ObservableFromAction<T> :
            Reuse.ObjectBase<ObservableFromAction<T>>,
            IObservableDisposable<T>,
            IReuseable.IOnRelease
        {
            public event Func<ObservableFromAction<T>, IObserverDisposableHandler<T>, IDisposable> OnSubscribeFunction;
            public event Action<ObservableFromAction<T>> OnDisposeAction;
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
