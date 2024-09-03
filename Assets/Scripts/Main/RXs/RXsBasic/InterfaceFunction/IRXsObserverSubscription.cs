using System;

namespace Main.RXs
{
    public interface IRXsObserverSubscription : IRXsObserver, IRXsSubscription
    {
        void AddSubscription(IRXsSubscription subscription);
        void RemoveSubscription(IRXsSubscription subscription, bool autoDispose = true);
    }
    public interface IRXsObserverSubscription<in T> : IRXsObserver<T>, IRXsSubscription, IRXsObserverSubscription { }
}