using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public class ObserverList<T> : IObserverList<T>
    {
        private readonly List<IObserverListSubscription<T>> observers = new();
        //IObserver
        public void OnCompleted() { foreach (var observer in observers) observer.OnCompletedToTyped<T>(); }
        public void OnError(Exception error) { foreach (var observer in observers) observer.OnErrorToTyped<T>(error); }
        public void OnNext(object value) => this.OnNextToTyped<T>(value);
        public void OnNext(T value) { foreach (var observer in observers) observer.OnNext(value); }
        public IDisposable Subscribe(System.IObserver<T> observer) => Subscribe(observer.ToSubscription());
        public IDisposable Subscribe(IObserver observer) => this.SubscribeToTyped<T>(observer);
        public IObserverListSubscription<T> Subscribe(IObserverListSubscription<T> observer)
        {
            if (observers.Contains(observer)) return observer;
            observers.Add(observer);
            observers.Sort(Comparison);
            return observer;
        }
        public void Unsubscribe(IObserverListSubscription<T> observer) => observers.Remove(observer);
        private static int Comparison(IObserverListSubscription<T> x, IObserverListSubscription<T> y) => x.Order.CompareTo(y.Order);
        public void Dispose()
        {
            foreach (var observer in observers) observer.Dispose();
        }
    }
}