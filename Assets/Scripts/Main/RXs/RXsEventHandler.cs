using System;

namespace Main.RXs
{
    public class RXsEventHandler<T> : ISubject<T>
    {
        protected ObserverNodeList<T> Observers { get; } = new();
        void IObserver<T>.OnCompleted() => OnCompleted();
        protected virtual void OnCompleted() => Observers.OnCompleted();
        void IObserver<T>.OnError(Exception error) => OnError(error);
        protected virtual void OnError(Exception error) => Observers.OnError(error);
        void IObserver<T>.OnNext(T value) => OnNext(value);
        protected virtual void OnNext(T value) => Observers.OnNext(value);
        public virtual IDisposable Subscribe(IObserver<T> observer) => Observers.Subscribe(observer);
        public virtual void Invoke(T eventArgs) => Observers.OnNext(eventArgs);
    }
}