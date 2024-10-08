﻿using System;

namespace Main.RXs
{
    partial class Observer
    {
        private class ObserverToNode<T> : ObserverNode<T>
        {
            private System.IObserver<T> observer;
            public ObserverToNode(System.IObserver<T> observer) => this.observer = observer;
            public override void OnNext(T value)
            {
                observer.OnNext(value);
                base.OnNext(value);
            }
            public override void OnCompleted()
            {
                observer.OnCompleted();
                base.OnCompleted();
            }
            public override void OnError(Exception error)
            {
                observer.OnError(error);
                base.OnError(error);
            }
            public override void Dispose()
            {
                if (observer is IDisposable disposable) disposable.Dispose();
                observer = null;
                base.Dispose();
            }
        }
        public static IObserverNode<T> ToNode<T>(this System.IObserver<T> observer)
        {
            if (observer is not IObserverNode<T> node) node = new ObserverToNode<T>(observer);
            return node;
        }
    }
}