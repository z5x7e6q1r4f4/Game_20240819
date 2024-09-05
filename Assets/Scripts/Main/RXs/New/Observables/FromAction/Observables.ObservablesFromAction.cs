using System;

namespace Main.RXs
{
    public static partial class Observables
    {
        private sealed class ObservablesFromAction<T> :
            Reuse.ObjectBase<ObservablesFromAction<T>>,
            IObservablesFromAction<T>,
            IReuseable.IOnRelease
        {
            public event Func<IObservablesFromAction<T>, IObserverSubscriptionHandler<T>, IDisposable> OnSubscribeFunction;
            public event Action<IObservablesFromAction<T>> OnDisposeAction;
            public void Dispose() => this.ReleaseToReusePool();
            void IReuseable.IOnRelease.OnRelease()
            {
                OnSubscribeFunction = null;
                OnDisposeAction?.Invoke(this);
                OnDisposeAction = null;
            }
            public IDisposable Subscribe(IObserver<T> observer) => OnSubscribeFunction?.Invoke(this, observer.ToSubscriptionHandler());
            public static ObservablesFromAction<T> GetFromReusePool() => GetFromReusePool(false);
        }
    }
}
