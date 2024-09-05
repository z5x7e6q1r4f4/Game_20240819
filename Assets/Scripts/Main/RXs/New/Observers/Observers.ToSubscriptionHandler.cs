using System;

namespace Main.RXs
{
    partial class Observers
    {
        public static IObserverSubscriptionHandler<T> ToSubscriptionHandler<T>(this IObserver<T> observer)
        {
            if (observer is not IObserverSubscriptionHandler<T> subscriptionHandler)
                subscriptionHandler = Create<T>(observer.OnNext, observer.OnCompleted, observer.OnError);
            return subscriptionHandler;
        }
    }
}