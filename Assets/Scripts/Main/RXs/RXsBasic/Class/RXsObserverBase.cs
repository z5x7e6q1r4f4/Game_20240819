using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public abstract class RXsObserverBase<T> :
        IRXsObserverSubscription<T>
    {
        private List<IRXsDisposable> subscriptions = new();
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
        void IObserver<T>.OnCompleted() => OnCompleted();
        void IObserver<T>.OnError(Exception error) => OnError(error);
        void IObserver<T>.OnNext(T value) => OnNext(value);
        void IRXsObserverSubscription.AddSubscription(IRXsDisposable subscription) => subscriptions.Add(subscription);
        void IRXsObserverSubscription.RemoveSubscription(IRXsDisposable subscription, bool autoDispose)
        { if (subscriptions.Remove(subscription) && autoDispose) subscription.Dispose(); }
    }
}