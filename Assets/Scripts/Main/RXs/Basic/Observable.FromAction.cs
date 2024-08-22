using System;

namespace Main.RXs
{
    partial class Observable
    {
        private class ObservableFromAction<T> : IObservable<T>
        {
            private readonly Action<System.IObserver<T>> action;
            public IDisposable Subscribe(System.IObserver<T> observer) 
            { action(observer); return Disposiable.Empty; }
            public IDisposable Subscribe(IObserver observer) => this.SubscribeToTyped<T>(observer);
            public ObservableFromAction(Action<System.IObserver<T>> action) => this.action = action;
        }
    }
}