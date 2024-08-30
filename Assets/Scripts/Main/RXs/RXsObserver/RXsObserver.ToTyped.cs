using System;

namespace Main.RXs
{
    public static partial class RXsObserver
    {
        private class ObserverToTyped<T> : RXsObserverItemReusable<ObserverToTyped<T>, T>
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
        public static IRXsObserver<T> ToTyped<T>(this IObserver observer)
        {
            if (observer is not IRXsObserver<T> typed) typed = ObserverToTyped<T>.GetFromReusePool(observer);
            return typed;
        }
    }
}