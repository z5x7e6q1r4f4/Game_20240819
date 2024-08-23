using System;

namespace Main.RXs
{
    public class ObserverNode<T> : IObserverNode<T>
    {
        IObserverNode<T> IObserverNode<T>.Previous { get; set; }
        IObserverNode<T> IObserverNode<T>.Next { get; set; }
        public ObserverNode(IObserverNode<T> previous = null, IObserverNode<T> next = null)
        {
            var node = this.ToNode();
            node.Previous = previous;
            node.Next = next;
        }
        //RXs.IObserver
        void IObserver.OnNext(object value) => this.OnNextToTyped<T>(value);
        void IObserver.OnCompleted() => this.OnCompletedToTyped<T>();
        void IObserver.OnError(Exception error) => this.OnErrorToTyped<T>(error);
        //System.IObserver<T>
        void System.IObserver<T>.OnNext(T value) => OnNext(value);
        protected virtual void OnNext(T value) => this.ToNode().Next?.OnNextToTyped<T>(value);
        void System.IObserver<T>.OnCompleted() => OnCompleted();
        protected virtual void OnCompleted() => this.ToNode().Next?.OnCompletedToTyped<T>();
        void System.IObserver<T>.OnError(Exception error) => OnError(error);
        protected virtual void OnError(Exception error) => this.ToNode().Next?.OnErrorToTyped<T>(error);
        //System.IDisposable
        void IDisposable.Dispose() => Dispose();
        protected virtual void Dispose() => this.DefaultDisposeObserverNode();
    }
}