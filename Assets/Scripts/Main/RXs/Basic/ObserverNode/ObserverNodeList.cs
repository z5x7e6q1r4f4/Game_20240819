using System;

namespace Main.RXs
{
    public class ObserverNodeList<T> : ISubject<T>
    {
        public readonly ObserverNode<T> First;
        public readonly ObserverNode<T> Last;
        public ObserverNodeList()
        {
            First = new();
            Last = new();
            First.Next = Last;
            Last.Previous = First;
        }
        public void OnCompleted() => First.OnCompleted();
        public void OnError(Exception error) => First.OnError(error);
        public void OnNext(T value) => First.OnNext(value);
        public IDisposable Subscribe(System.IObserver<T> observer)
        {
            var node = observer.ToNode();
            //Self
            node.Next = Last;
            node.Previous = Last.Previous;
            //Last
            Last.Previous.Next = node;
            Last.Previous = node;
            return node;
        }
        void IObserver.OnNext(object value) => this.OnNextToTyped<T>(value);
        IDisposable IObservable.Subscribe(IObserver observer) => this.SubscribeToTyped<T>(observer);
    }
}