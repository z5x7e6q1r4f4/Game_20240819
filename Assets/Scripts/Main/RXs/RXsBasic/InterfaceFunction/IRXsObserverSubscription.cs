using System;

namespace Main.RXs
{
    public interface IRXsObserverSubscription : IRXsObserver, IRXsDisposable
    {
        void AddSubscription(IRXsDisposable subscription);
        void RemoveSubscription(IRXsDisposable subscription, bool autoDispose = true);
    }
    public interface IRXsObserverSubscription<in T> : IRXsObserver<T>, IRXsDisposable, IRXsObserverSubscription { }
}