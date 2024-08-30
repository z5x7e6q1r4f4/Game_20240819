using System;

namespace Main.RXs
{
    public abstract class ObserverListSubscription<T> : IObserverListSubscription<T>
    {
        int IObserverOrderable.Order
        { get => index; set => index = value; }
        private int index;
        IObserverList<T> IObserverListSubscription<T>.ObserverList
        { get => observerList; set => observerList = value; }
        private IObserverList<T> observerList;
        void System.IObserver<T>.OnNext(T value) => OnNext(value);
        protected abstract void OnNext(T value);
        void System.IObserver<T>.OnCompleted() => OnCompleted();
        protected abstract void OnCompleted();
        void System.IObserver<T>.OnError(Exception error) => OnError(error);
        protected abstract void OnError(Exception error);
        void IObserver.OnNext(object value) => this.OnNextToTyped<T>(value);
        void IObserver.OnCompleted() => this.OnCompletedToTyped<T>();
        void IObserver.OnError(Exception error) => this.OnErrorToTyped<T>(error);
        void IDisposable.Dispose() => Dispose();
        protected virtual void Dispose()
        {
            Unsubscribe();
            observerList = null;
            index = default;
        }
        void IObserverListSubscription<T>.Subscribe() => Subscribe();
        private void Subscribe() => observerList?.SubscribeToTyped(this);
        void IObserverListSubscription<T>.Unsubscribe() => Unsubscribe();
        private void Unsubscribe() => observerList?.Unsubscribe(this);
    }
}