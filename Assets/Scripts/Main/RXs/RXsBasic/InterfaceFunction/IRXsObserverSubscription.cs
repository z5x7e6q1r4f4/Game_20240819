using System;

namespace Main.RXs
{
    public interface IRXsObserverSubscription : IRXsObserver, IRXsSubscription
    {
        IRXsSubscription Subscription { get; set; }
    }
    public interface IRXsObserverSubscription<in T> : IRXsObserver<T>, IRXsSubscription, IRXsObserverSubscription { }
}