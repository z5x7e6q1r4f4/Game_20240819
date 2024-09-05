using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public abstract class RXsObserverBase<T> :
        IRXsObserverSubscription<T>
    {
        private List<IRXsSubscription> subscriptions = new();
        protected bool hasDisposed;
        void IDisposable.Dispose()
        {
            if (hasDisposed) throw new Exception();
            Dispose();
        }

        protected virtual void Dispose()
        {
            foreach (var subscription in subscriptions) subscription.Dispose();
            subscriptions.Clear();
        }
        void IRXsObserver.OnCompleted() => OnCompleted();
        protected abstract void OnCompleted();
        void IRXsObserver.OnError(Exception error) => OnError(error);
        protected abstract void OnError(Exception error);
        void IRXsObserver.OnNext(object value) => OnNext((T)value);
        protected abstract void OnNext(T value);
        void IRXsSubscription.Subscribe() => Subscribe();
        protected virtual void Subscribe() { foreach (var subscription in subscriptions) subscription.Subscribe(); }
        void IRXsSubscription.Unsubscribe() => Unsubscribe();
        protected virtual void Unsubscribe() { foreach (var subscription in subscriptions) subscription.Unsubscribe(); }
        void IObserver<T>.OnCompleted() => OnCompleted();
        void IObserver<T>.OnError(Exception error) => OnError(error);
        void IObserver<T>.OnNext(T value) => OnNext(value);
        void IRXsObserverSubscription.AddSubscription(IRXsSubscription subscription) => subscriptions.Add(subscription);
        void IRXsObserverSubscription.RemoveSubscription(IRXsSubscription subscription, bool autoDispose)
        { if (subscriptions.Remove(subscription) && autoDispose) subscription.Dispose(); }
    }
}