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
            First.ToNode().Next = Last;
            Last.ToNode().Previous = First;
        }
        public void OnCompleted() => First.OnCompletedToTyped<T>();
        public void OnError(Exception error) => First.OnErrorToTyped<T>(error);
        public void OnNext(T value) => First.OnNextToTyped<T>(value);
        public IDisposable Subscribe(System.IObserver<T> observer)
        {
            var node = observer.ToNode();
            //Self
            node.Next = Last;
            node.Previous = Last.ToNode().Previous;
            //Last
            Last.ToNode().Previous.Next = node;
            Last.ToNode().Previous = node;
            return node;
        }
        void IObserver.OnNext(object value) => this.OnNextToTyped<T>(value);
        IDisposable IObservable.Subscribe(IObserver observer) => this.SubscribeToTyped<T>(observer);
    }
}