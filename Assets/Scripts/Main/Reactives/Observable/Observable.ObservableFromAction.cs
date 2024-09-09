using System;

namespace Main
{
    partial class Observable
    {
        public sealed class ObservableFromAction<T> :
            Reuse.ObjectBase<ObservableFromAction<T>>,
            IObservable<T>,
            IDisposable,
            IReuseable.IOnRelease
        {
            public event Func<ObservableFromAction<T>, IObserverBase<T>, IDisposable> OnSubscribeFunction;
            public event Action<ObservableFromAction<T>> OnDisposeAction;
            public void Dispose() => this.ReleaseToReusePool();
            void IReuseable.IOnRelease.OnRelease()
            {
                OnSubscribeFunction = null;
                OnDisposeAction?.Invoke(this);
                OnDisposeAction = null;
            }
            public IDisposable Subscribe(IObserver<T> observer)
            {
                var disposableHanlder = observer.AsObserverBase();
                var disposable = OnSubscribeFunction?.Invoke(this, disposableHanlder);
                if (disposable == null)
                {
                    disposable = Disposable.Create(disposable => disposableHanlder.RemoveSubscription(disposable));
                    disposableHanlder.AddSubscription(disposable);
                }
                return disposable;
            }

            public static ObservableFromAction<T> GetFromReusePool() => GetFromReusePool(false);
        }
    }
}
