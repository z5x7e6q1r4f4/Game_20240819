using System;

namespace Main.RXs
{
    partial class RXsObservable
    {
        private class ObservableFromAction<T> :
            Reuse.ObjectBase<ObservableFromAction<T>>,
            IRXsObservable<T>,
            IReuseable.IOnRelease
        {
            private Action<IObserver<T>> action;
            public IDisposable Subscribe(IObserver<T> observer)
            {
                action(observer);
                if (observer is not IDisposable disposable) disposable = Disposiable.Empty;
                return disposable;
            }
            public IDisposable Subscribe(IObserver observer) => this.SubscribeToTyped<T>(observer);
            public static ObservableFromAction<T> GetFromReusePool(Action<IObserver<T>> action)
            {
                var observable = GetFromReusePool();
                observable.action = action;
                return observable;
            }
            void IReuseable.IOnRelease.OnRelease() => action = null;
            void IDisposable.Dispose() => this.ReleaseToReusePool();
        }
        public static IRXsObservable<T> FromAction<T>(Action<IObserver<T>> action)
            => ObservableFromAction<T>.GetFromReusePool(action);
    }
}