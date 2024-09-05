using System;

namespace Main.RXs
{
    public interface ISubscriptionHandler : IDisposable
    {
        int Count { get; }
        void Add(ISubscription subscription);
        bool Remove(ISubscription subscription);
    }
}