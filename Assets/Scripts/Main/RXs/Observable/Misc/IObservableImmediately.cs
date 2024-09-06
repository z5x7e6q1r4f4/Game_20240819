using System;

namespace Main.RXs
{
    public interface IObservableImmediately<out T> : IObservable<T>
    {
        Action<IObserver<T>> ImmediatelyAction { get; }
    }
}