using System;

namespace Main.RXs
{
    public interface IObservableImmediately<out T> : IObservable<T>
    {
        IObservable<T> Immediately();
    }
}