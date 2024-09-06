using System;

namespace Main.RXs
{
    partial class Observer
    {
        public abstract class ObserverBase<T> :
            Disposable.DisposableBase,
            IObserverDisposableHandler<T>
        {
            int IObserverOrderable.Order { get; set; }
            void IObserver<T>.OnCompleted() => OnCompleted();
            protected abstract void OnCompleted();
            void IObserver<T>.OnError(Exception error) => OnError(error);
            protected abstract void OnError(Exception error);
            void IObserver<T>.OnNext(T value) => OnNext(value);
            protected abstract void OnNext(T value);
        }
    }
}