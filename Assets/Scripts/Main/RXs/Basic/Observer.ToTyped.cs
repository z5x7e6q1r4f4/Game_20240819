using System;

namespace Main.RXs
{
    public static partial class Observer 
    {
        private class ObserverToTyped<T> : ObserverNode<T>
        {
            private IObserver Observer { get; }
            public override void OnCompleted() { Observer.OnCompleted(); base.OnCompleted(); }
            public override void OnError(Exception error) { Observer.OnError(error); base.OnError(error); }
            public override void OnNext(T value) { Observer.OnNext(value); base.OnNext(value); }
            public ObserverToTyped(IObserver observer) => Observer = observer;
        }
        public static IObserver<T> ToTyped<T>(this IObserver observer)
        {
            if (observer is not IObserver<T> typed) typed = new ObserverToTyped<T>(observer);
            return typed;
        }
    }
}