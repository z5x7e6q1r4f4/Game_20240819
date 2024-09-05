using System;
using System.Collections.Generic;
namespace Main.RXs
{
    public sealed class ObserverList<T> : IObserver<T>, IObservable<T>
    {
        private readonly List<ISubscription> subscriptions = new();
        private readonly List<IObserverSubscriptionHandler<T>> observers = new();
        public void OnCompleted()
        { foreach (var observer in observers) observer.OnCompleted(); }
        public void OnError(Exception error)
        { foreach (var observer in observers) observer.OnError(error); }
        public void OnNext(T value)
        { foreach (var observer in observers) observer.OnNext(value); }
        public IDisposable Subscribe(IObserver<T> observer)
        {
            var subscriptionHandler = observer.ToSubscriptionHandler();
            var subscription = Subscriptions.Create((subscription) =>
            {
                observers.Remove(subscriptionHandler);
                subscriptions.Remove(subscription);
                subscriptionHandler.RemoveAndDispose(subscription);
            });
            subscriptionHandler.Add(subscription);
            subscriptions.Add(subscription);
            return subscription;
        }
        public void Clear()
        {
            using var subscriptions = this.subscriptions.ToReuseList();
            foreach (var subscription in subscriptions) subscription.Dispose();
            if (this.subscriptions.Count > 0) throw new Exception("Not all subscription clear");
            if (observers.Count > 0) throw new Exception("Not all observer clear");
        }
    }
}