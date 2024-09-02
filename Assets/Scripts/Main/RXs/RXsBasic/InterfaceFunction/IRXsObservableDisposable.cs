using System;

namespace Main.RXs
{
    public interface IRXsObservableDisposable : IRXsObservable, IDisposable { }
    public interface IRXsObservableDisposable<out T> : IRXsObservable<T>, IDisposable, IRXsObservableDisposable { }
}