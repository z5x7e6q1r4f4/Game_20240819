using Main.RXs;
using System;
using UnityEngine;

namespace Main.RXs
{
    public static partial class Observables
    {
        public static IObservablesFromAction<T> Create<T>(
            Func<IObservablesFromAction<T>, IObserverSubscriptionHandler<T>, IDisposable> onSubscribe = null,
            Action<IObservablesFromAction<T>> onDispose = null)
        {
            var observable = ObservablesFromAction<T>.GetFromReusePool();
            observable.OnSubscribeFunction += onSubscribe;
            observable.OnDisposeAction += onDispose;
            return observable;
        }
        public static IObservablesFromAction<T> Create<T>(
         Func<IObserverSubscriptionHandler<T>, IDisposable> onSubscribe = null,
         Action onDispose = null)
            => Create<T>(
                onSubscribe != null ? (_, observer) => onSubscribe(observer) : null,
                onDispose != null ? _ => onDispose() : null);
        public static IObservablesFromAction<T> Create<T>()
            => Create(default(Func<IObservablesFromAction<T>, IObserverSubscriptionHandler<T>, IDisposable>));
    }
}
