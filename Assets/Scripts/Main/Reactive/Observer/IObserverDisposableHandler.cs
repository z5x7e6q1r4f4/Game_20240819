using System;

namespace Main
{
    public interface IObserverDisposableHandler<in T> : IDisposableHandler, IObserver<T>, IObserverOrderable { }
}