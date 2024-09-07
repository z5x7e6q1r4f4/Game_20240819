using NUnit.Framework;
using System;
using System.Collections.Generic;
namespace Main
{
    public sealed class ObserverList<T> : IObserver<T>, IObservable<T>
    {
        private readonly List<IDisposable> disposables = new();//subscription
        private readonly List<IObserverDisposableHandler<T>> observers = new();
        public void OnCompleted()
        {
            using var observers = this.observers.ToReuseList();
            foreach (var observer in observers) if (this.observers.Contains(observer)) observer.OnCompleted();
        }
        public void OnError(Exception error)
        {
            using var observers = this.observers.ToReuseList();
            foreach (var observer in observers) if (this.observers.Contains(observer)) observer.OnError(error);
        }
        public void OnNext(T value)
        {
            using var observers = this.observers.ToReuseList();
            foreach (var observer in observers) if (this.observers.Contains(observer)) observer.OnNext(value);
        }
        public IDisposable Subscribe(IObserver<T> observer)
        {
            var disposableHandler = observer.ToDisposableHandler();
            var disposable = Disposable.Create(disposable =>
            {
                observers.Remove(disposableHandler);//移除Observer
                disposables.Remove(disposable);//不會再次被List方Dispose
                disposableHandler.RemoveAndDispose(disposable);//移除Disposable並確認是否可以DisposeObserver
            });
            observers.Add(disposableHandler);
            disposables.Add(disposable);
            disposableHandler.Add(disposable);
            observers.Sort((x, y) => x.Order.CompareTo(y.Order));//Orderable
            return disposable;
        }
        public void Clear()
        {
            using var disposables = this.disposables.ToReuseList();
            foreach (var subscription in disposables) subscription.Dispose();
            Assert.AreEqual(0, this.disposables.Count, "not all disposable cleared");
            Assert.AreEqual(0, observers.Count, "not all observer cleared");
        }
    }
}