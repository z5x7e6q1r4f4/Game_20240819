using System;

namespace Main.RXs
{
    public static partial class Observer
    {
        private class ObserverToTyped<T> : ObserverListSubscriptionReusable<ObserverToTyped<T>, T>
        {
            private IObserver observer;
            protected override void OnCompleted() => observer.OnCompleted();
            protected override void OnError(Exception error) => observer.OnError(error);
            protected override void OnNext(T value) => observer.OnNext(value);
            public static ObserverToTyped<T> GetFromReusePool(IObserver observer)
            {
                var node = GetFromReusePool();
                node.observer = observer;
                return node;
            }
            protected override void OnRelease()
            {
                (observer as IDisposable)?.Dispose();
                base.OnRelease();
            }
        }
        public static IObserver<T> ToTyped<T>(this IObserver observer)
        {
            if (observer is not IObserver<T> typed) typed = ObserverToTyped<T>.GetFromReusePool(observer);
            return typed;
        }
    }
}