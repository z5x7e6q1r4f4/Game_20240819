using System;

namespace Main.RXs
{
    public class ObservableEventHandler<T> : IObservable<T>
    {
        protected ObserverList<T> Observers { get; } = new();
        public IDisposable Subscribe(IObserver<T> observer) => Observers.Subscribe(observer);
        public void Clear() => Observers.Clear();
        public void Invoke(T value) => Observers.OnNext(value);
    }
}