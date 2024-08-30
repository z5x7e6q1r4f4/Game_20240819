using System;

namespace Main.RXs
{
    public interface IRXsEventHandler<T> :
        IRXsSubject<T>,
        IDisposable,
        IObservableImmediately<T>
    { }
}