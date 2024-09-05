using System;

namespace Main.RXs
{
    public static partial class Observables
    {
        public interface IObservablesFromAction<T> : IObservable<T>, IDisposable
        {
            event Func<IObservablesFromAction<T>, IObserverSubscriptionHandler<T>, IDisposable> OnSubscribeFunction;
            event Action<IObservablesFromAction<T>> OnDisposeAction;
        }
    }
}
