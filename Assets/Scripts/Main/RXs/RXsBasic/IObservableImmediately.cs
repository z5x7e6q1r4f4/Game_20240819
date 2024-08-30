using System;

namespace Main.RXs
{
    public interface IObservableImmediately<out T> : IRXsObservable<T>
    {
        Action<IObserver<T>> ImmediatelyAction { get; }
    }
}