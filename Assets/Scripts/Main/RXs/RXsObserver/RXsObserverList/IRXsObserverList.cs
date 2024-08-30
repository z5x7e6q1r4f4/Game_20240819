using System;

namespace Main.RXs
{
    public interface IRXsObserverList<T> : IRXsSubject<T>
    {
        IRXsObserverItem<T> Subscribe(IRXsObserverItem<T> observer);
        void Unsubscribe(IRXsObserverItem<T> observer);
    }
}