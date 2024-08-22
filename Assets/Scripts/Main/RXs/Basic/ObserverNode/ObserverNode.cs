using System;

namespace Main.RXs
{
    public class ObserverNode<T> : IObserverNode<T>
    {
        public IObserverNode<T> Previous { get; set; }
        public IObserverNode<T> Next { get; set; }
        public ObserverNode(IObserverNode<T> previous = null, IObserverNode<T> next = null)
        {
            Previous = previous;
            Next = next;
        }
        void IObserver.OnNext(object value) => this.OnNextToTyped<T>(value);
        public virtual void OnNext(T value) => Next?.OnNextToTyped<T>(value);
        public virtual void OnCompleted() => Next?.OnCompletedToTyped<T>();
        public virtual void OnError(Exception error) => Next?.OnErrorToTyped<T>(error);
        public virtual void Dispose()
        {
            if (Previous != null) Previous.Next = Next;
            if (Next != null) Next.Previous = Previous;
        }
    }
}