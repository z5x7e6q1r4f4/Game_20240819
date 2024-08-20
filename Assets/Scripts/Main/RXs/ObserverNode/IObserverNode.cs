using System;

namespace Main.RXs
{
    public interface IObserverNode<T> : IObserver<T>, IDisposable
    {
        IObserverNode<T> Previous { get; set; }
        IObserverNode<T> Next { get; set; }
    }
}