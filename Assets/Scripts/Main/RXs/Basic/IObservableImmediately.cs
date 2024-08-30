using System;

namespace Main.RXs
{
    public interface IObservableImmediately<out T> : IObservable<T>
    {
        Action<System.IObserver<T>> ImmediatelyAction { get; }
    }
}