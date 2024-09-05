using System;

namespace Main.RXs
{
    public static partial class Observers
    {
        public interface IObserverFromAction<T> : IObserverSubscriptionHandler<T>, IDisposable
        {
            event Action<IObserverFromAction<T>, T> OnNextAction;
            event Action<IObserverFromAction<T>, Exception> OnErrorAction;
            event Action<IObserverFromAction<T>> OnCompletedAction;
            event Action<IObserverFromAction<T>> OnDisposeAction;
        }
    }
}