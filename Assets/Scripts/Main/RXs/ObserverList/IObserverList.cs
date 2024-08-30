using System;

namespace Main.RXs
{
    public interface IObserverList<T> :
        ISubject<T>,
        IDisposable
    {
        IObserverListSubscription<T> Subscribe(IObserverListSubscription<T> observer);
        void Unsubscribe(IObserverListSubscription<T> observer);
    }
}