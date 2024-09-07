using System;

namespace Main
{
    public class EventHandler<T> : IObservable<T>, IObservableImmediately<T>, IDisposable
    {
        protected ObserverList<T> Observers { get; } = new();
        public Action<IObserver<T>> ImmediatelyAction { get; }
        public IDisposable Subscribe(IObserver<T> observer) => Observers.Subscribe(observer);
        public void Clear() => Observers.Clear();
        public void Invoke(T value) => Observers.OnNext(value);
        public EventHandler(Action<IObserver<T>> immediatelyAction = null) => ImmediatelyAction = immediatelyAction;
        void IDisposable.Dispose()=>Clear();

    }
}