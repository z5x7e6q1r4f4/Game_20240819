using System;

namespace Main.RXs
{
    public abstract class RXsObserverBase<T> :
        IRXsObserverSubscription<T>
    {
        IRXsSubscription IRXsObserverSubscription.Subscription { get => subscription; set => subscription = value; }
        protected IRXsSubscription subscription;
        void IDisposable.Dispose() => Dispose();
        protected virtual void Dispose()
        {
            subscription?.Dispose();
            subscription = null;
        }
        void IRXsObserver.OnCompleted() => OnCompleted();
        protected abstract void OnCompleted();
        void IRXsObserver.OnError(Exception error) => OnError(error);
        protected abstract void OnError(Exception error);
        void IRXsObserver.OnNext(object value) => OnNext((T)value);
        protected abstract void OnNext(T value);
        void IRXsSubscription.Subscribe() => Subscribe();
        protected virtual void Subscribe() => subscription.Subscribe();
        void IRXsSubscription.Unsubscribe() => Unsubscribe();
        protected virtual void Unsubscribe() => subscription.Unsubscribe();
        void IObserver<T>.OnCompleted() => OnCompleted();
        void IObserver<T>.OnError(Exception error) => OnError(error);
        void IObserver<T>.OnNext(T value) => OnNext(value);
    }
}