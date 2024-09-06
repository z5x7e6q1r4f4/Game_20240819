using System;

namespace Main.RXs
{
    public interface IObserverDisposableHandler<in T> : IDisposableContainer, IObserver<T>, IObserverOrderable { }
}