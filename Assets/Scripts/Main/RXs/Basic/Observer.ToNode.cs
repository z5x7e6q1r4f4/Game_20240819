using System;

namespace Main.RXs
{
    partial class Observer
    {
        private class ObserverToNode<T> : ObserverNodeReusable<T, ObserverToNode<T>>
        {
            private System.IObserver<T> observer;
            public static ObserverToNode<T> GetFromReusePool(System.IObserver<T> observer)
            {
                var node = GetFromReusePool();
                node.observer = observer;
                return node;
            }
            protected override void OnNext(T value)
            {
                observer.OnNext(value);
                base.OnNext(value);
            }
            protected override void OnCompleted()
            {
                observer.OnCompleted();
                base.OnCompleted();
            }
            protected override void OnError(Exception error)
            {
                observer.OnError(error);
                base.OnError(error);
            }
            protected override void OnRelease()
            {
                if (observer is IDisposable disposable) disposable.Dispose();
                observer = null;
                base.OnRelease();
            }
        }
        public static IObserverNode<T> ToNode<T>(this System.IObserver<T> observer)
        {
            if (observer is not IObserverNode<T> node) node = ObserverToNode<T>.GetFromReusePool(observer);
            return node;
        }
        public static void DefaultDisposeObserverNode<T>(this IObserverNode<T> node)
        {
            if (node.Previous != null) node.Previous.Next = node.Next;
            if (node.Next != null) node.Next.Previous = node.Previous;
        }
    }
}