using System;

namespace Main
{
    partial class Observer
    {
        public static IObserverDisposableHandler<T> ToDisposableHandler<T>(this IObserver<T> observer)
        {
            if (observer is not IObserverDisposableHandler<T> subscriptionHandler)
                subscriptionHandler = Create<T>(observer.OnNext, observer.OnCompleted, observer.OnError);
            return subscriptionHandler;
        }
    }
}