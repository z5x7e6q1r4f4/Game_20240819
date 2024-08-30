using System;

namespace Main.RXs
{
    public class RXsEventHandler<T> : IRXsEventHandler<T>
    {
        protected RXsObserverList<T> Observers { get; } = new();
        Action<IObserver<T>> IObservableImmediately<T>.ImmediatelyAction => immediatelyAction;
        private readonly Action<IObserver<T>> immediatelyAction;
        public RXsEventHandler(Action<IObserver<T>> immediatelyAction = null) => this.immediatelyAction = immediatelyAction;
        void IObserver<T>.OnCompleted() => OnCompleted();
        protected virtual void OnCompleted() => Observers.OnCompleted();
        void IObserver<T>.OnError(Exception error) => OnError(error);
        protected virtual void OnError(Exception error) => Observers.OnError(error);
        void IObserver<T>.OnNext(T value) => OnNext(value);
        protected virtual void OnNext(T value) => Observers.OnNext(value);
        public virtual IDisposable Subscribe(IObserver<T> observer) => Observers.Subscribe(observer);
        public virtual void Invoke(T eventArgs) => Observers.OnNext(eventArgs);
        //
        void IObserver.OnNext(object value) => this.OnNextToTyped<T>(value);
        void IObserver.OnCompleted() => this.OnCompletedToTyped<T>();
        void IObserver.OnError(Exception error) => this.OnErrorToTyped<T>(error);
        IDisposable IObservable.Subscribe(IObserver observer) => this.SubscribeToTyped<T>(observer);
        public virtual void Dispose() => Observers.Dispose();
    }
}