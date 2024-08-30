using System;

namespace Main.RXs
{
    public interface IObserverListSubscription<T> : IObserverOrderable<T>, IDisposable
    {
        IObserverList<T> ObserverList { get; set; }
        void Subscribe();
        void Unsubscribe();
    }
}