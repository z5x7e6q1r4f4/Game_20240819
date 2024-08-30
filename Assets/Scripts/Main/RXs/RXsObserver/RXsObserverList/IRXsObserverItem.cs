using System;

namespace Main.RXs
{
    public interface IRXsObserverItem<T> : IObserverOrderable<T>, IDisposable
    {
        IRXsObserverList<T> ObserverList { get; set; }
        void Subscribe();
        void Unsubscribe();
    }
}