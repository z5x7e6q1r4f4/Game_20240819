using System;

namespace Main.RXs
{
    public static partial class Observable
    {
        public interface IObservableFromAction<T> : IObservableDisposable<T>
        {
            event Func<IObservableFromAction<T>, IObserverDisposableHandler<T>, IDisposable> OnSubscribeFunction;
            event Action<IObservableFromAction<T>> OnDisposeAction;
        }
    }
}
