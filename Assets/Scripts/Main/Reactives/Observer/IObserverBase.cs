using System;
using System.Collections.Generic;

namespace Main
{
    public interface IObserverBase<in T> :
        IObserver<T>,
        IDisposableBase
    {
        int Order { get; set; }
        List<IDisposable> Subscriptions { get; }
    }
}