using System;

namespace Main.RXs
{
    public interface IObservable { IDisposable Subscribe(IObserver observer); }
    public interface IRXsObservable<out T> : IObservable<T>, IObservable, IDisposable { }
}