using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Main
{
    public abstract class ObserverBase<T> :
        DisposableBase,
        IObserverBase<T>
    {
        int IObserverBase<T>.Order { get; set; }
        List<IDisposable> IObserverBase<T>.Subscriptions => Subscriptions;
        private readonly List<IDisposable> Subscriptions = new();
        void IObserver<T>.OnCompleted() => OnCompleted();
        protected abstract void OnCompleted();
        void IObserver<T>.OnError(Exception error) => OnError(error);
        protected abstract void OnError(Exception error);
        void IObserver<T>.OnNext(T value) => OnNext(value);
        protected abstract void OnNext(T value);
        protected override void OnDispose()
        {
            base.OnDispose();
            using var subscriptions = Subscriptions.ToReuseList();
            foreach (var subscription in subscriptions) subscription.Dispose();
            Assert.That(Subscriptions.Count == 0);
        }
    }
}