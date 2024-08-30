using System;

namespace Main.RXs
{
    public interface IRXsEventHandler<T> :
        ISubject<T>,
        IDisposable,
        IObservableImmediately<T>
    { }
}