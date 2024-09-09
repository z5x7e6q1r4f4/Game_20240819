using System;

namespace Main
{
    public static partial class Observer
    {
        public sealed class ObserverFromAction<T> :
            ObserverBaseReuseable<ObserverFromAction<T>, T>,
            IReuseable.IOnRelease
        {
            public event Action<ObserverFromAction<T>, T> OnNextAction;
            public event Action<ObserverFromAction<T>, Exception> OnErrorAction;
            public event Action<ObserverFromAction<T>> OnCompletedAction;
            protected override void OnCompleted() => OnCompletedAction?.Invoke(this);
            protected override void OnError(Exception error) => OnErrorAction?.Invoke(this, error);
            protected override void OnNext(T value) => OnNextAction?.Invoke(this, value);
            protected override void OnRelease()
            {
                OnNextAction = null;
                OnCompletedAction = null;
                OnErrorAction = null;
                base.OnRelease();
            }
            public static ObserverFromAction<T> GetFromReusePool() => GetFromReusePool(false);
        }
    }
}