using System;

namespace Main.RXs
{
    public interface ISubject<in TObserver, out TObservable> : IObserver<TObserver>, IObservable<TObservable> { }
    public interface ISubject<T> : ISubject<T, T> { }
}