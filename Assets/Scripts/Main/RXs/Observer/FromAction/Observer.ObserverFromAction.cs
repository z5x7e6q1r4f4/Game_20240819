using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public static partial class Observer
    {
        private sealed class ObserverFromAction<T> :
            ObserverBaseReuseable<ObserverFromAction<T>, T>,
            IObserverFromAction<T>,
            IReuseable.IOnRelease
        {
            public event Action<IObserverFromAction<T>, T> OnNextAction;
            public event Action<IObserverFromAction<T>, Exception> OnErrorAction;
            public event Action<IObserverFromAction<T>> OnCompletedAction;
            public event Action<IObserverFromAction<T>> OnDisposeAction;
            protected override void OnCompleted() => OnCompletedAction?.Invoke(this);
            protected override void OnError(Exception error) => OnErrorAction?.Invoke(this, error);
            protected override void OnNext(T value) => OnNextAction?.Invoke(this, value);
            protected override void OnRelease()
            {
                OnNextAction = null;
                OnCompletedAction = null;
                OnErrorAction = null;
                OnDisposeAction?.Invoke(this);
                OnDisposeAction = null;
                base.OnRelease();
            }
            public static ObserverFromAction<T> GetFromReusePool() => GetFromReusePool(false);
        }
    }
}