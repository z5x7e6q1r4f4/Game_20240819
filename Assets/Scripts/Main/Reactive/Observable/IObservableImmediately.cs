using System;

namespace Main
{
    public interface IObservableImmediately<out T> : IObservable<T>
    {
        Action<IObserver<T>> ImmediatelyAction { get; }
    }
}