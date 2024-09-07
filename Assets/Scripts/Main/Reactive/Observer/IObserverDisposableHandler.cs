using System;

namespace Main
{
    public interface IObserverDisposableHandler<in T> : IDisposableContainer, IObserver<T>, IObserverOrderable { }
}