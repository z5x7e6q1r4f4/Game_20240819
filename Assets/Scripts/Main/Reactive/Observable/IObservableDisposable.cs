using System;

namespace Main
{
    public interface IObservableDisposable<out T> : IObservable<T>, IDisposable { }
}
