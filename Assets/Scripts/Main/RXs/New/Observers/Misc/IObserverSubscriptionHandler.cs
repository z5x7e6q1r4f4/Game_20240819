using System;

namespace Main.RXs
{
    public interface IObserverSubscriptionHandler<in T> : ISubscriptionHandler, IObserver<T>, IObserverOrderable { }
}