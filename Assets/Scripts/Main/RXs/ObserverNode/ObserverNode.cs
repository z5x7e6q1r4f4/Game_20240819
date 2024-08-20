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

        public virtual void OnNext(T value) => Next?.OnNext(value);
        public virtual void OnCompleted() => Next?.OnCompleted();
        public virtual void OnError(Exception error) => Next?.OnError(error);
        public virtual void Dispose()
        {
            if (Previous != null) Previous.Next = Next;
            if (Next != null) Next.Previous = Previous;
        }
    }
}