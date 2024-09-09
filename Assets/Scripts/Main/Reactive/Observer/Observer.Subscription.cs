using System;

namespace Main
{
    partial class Observer
    {
        public static IObserverBase<T> AsObserverBase<T>(this IObserver<T> observer)
        {
            if (observer is not IObserverBase<T> subscriptionHandler)
                subscriptionHandler = Create<T>(observer.OnNext, observer.OnCompleted, observer.OnError);
            return subscriptionHandler;
        }
        public static void AddSubscription<T>(this IObserverBase<T> subscriptionHandler, IDisposable subscription)
        {
            if (!subscriptionHandler.Subscriptions.Contains(subscription))
                subscriptionHandler.Subscriptions.Add(subscription);
        }
        public static void RemoveSubscription<T>(this IObserverBase<T> subscriptionHandler, IDisposable subscription, bool tryDispose = true)
        {
            if (subscriptionHandler.Subscriptions.Remove(subscription) &&
                tryDispose &&
                subscriptionHandler.Subscriptions.Count == 0)
                subscriptionHandler.Dispose();
        }
    }
}