using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public static partial class Observers
    {
        private sealed class ObserverFromAction<T> :
            Reuse.ObjectBase<ObserverFromAction<T>>,
            IObserverFromAction<T>,
            IReuseable.IOnRelease
        {
            public int Count => throw new NotImplementedException();
            public int Order { get; set; }
            public event Action<IObserverFromAction<T>, T> OnNextAction;
            public event Action<IObserverFromAction<T>, Exception> OnErrorAction;
            public event Action<IObserverFromAction<T>> OnCompletedAction;
            public event Action<IObserverFromAction<T>> OnDisposeAction;
            private readonly List<ISubscription> subscriptions = new();
            public void Add(ISubscription subscription) => subscriptions.Add(subscription);
            public bool Remove(ISubscription subscription) => subscriptions.Remove(subscription);
            public void Dispose() => this.ReleaseToReusePool();
            public void OnCompleted() => OnCompletedAction?.Invoke(this);
            public void OnError(Exception error) => OnErrorAction?.Invoke(this, error);
            public void OnNext(T value) => OnNextAction?.Invoke(this, value);
            void IReuseable.IOnRelease.OnRelease()
            {
                using var subscriptions = this.subscriptions.ToReuseList();
                foreach (var subscription in subscriptions) subscription.Dispose();
                this.subscriptions.Clear();
                OnNextAction = null;
                OnCompletedAction = null;
                OnErrorAction = null;
                OnDisposeAction?.Invoke(this);
                OnDisposeAction = null;
            }
            public static ObserverFromAction<T> GetFromReusePool() => GetFromReusePool(false);
        }
    }
}