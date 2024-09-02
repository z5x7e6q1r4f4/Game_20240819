using System;

namespace Main.RXs
{
    partial class RXsObservable
    {
        private class ObservableFromAction<T> :
            Reuse.ObjectBase<ObservableFromAction<T>>,
            IRXsObservableDisposable<T>,
            IReuseable.IOnRelease
        {
            private Func<IRXsObservableDisposable<T>, IRXsObserverSubscription<T>, IRXsSubscription> action;
            private Action onRelease;
            public IRXsSubscription Subscribe(IRXsObserver<T> observer) => Subscribe(observer.ToRXsObserverSubscription());
            public IDisposable Subscribe(IObserver<T> observer) => Subscribe(observer.ToRXsObserverSubscription());
            public IRXsSubscription Subscribe(IRXsObserver observer) => Subscribe(observer.ToRXsObserverSubscription<T>());
            protected IRXsSubscription Subscribe(IRXsObserverSubscription<T> observer) => action(this, observer);
            public static ObservableFromAction<T> GetFromReusePool(Func<IRXsObservableDisposable<T>, IRXsObserverSubscription<T>, IRXsSubscription> action, Action onRelease)
            {
                var observable = GetFromReusePool();
                observable.action = action;
                observable.onRelease = onRelease;
                return observable;
            }
            void IReuseable.IOnRelease.OnRelease()
            {
                onRelease?.Invoke();
                onRelease = null;
                action = null;
            }
            void IDisposable.Dispose() => this.ReleaseToReusePool();
        }
        //Typed
        public static IRXsObservableDisposable<T> FromAction<T>(Func<IRXsObservableDisposable<T>, IRXsObserverSubscription<T>, IRXsSubscription> action, Action onRelease = null)
            => ObservableFromAction<T>.GetFromReusePool(action, onRelease);
        public static IRXsObservableDisposable<T> FromAction<T>(Func<IRXsObserverSubscription<T>, IRXsSubscription> action, Action onRelease = null)
            => FromAction<T>((_, o) => action?.Invoke(o), onRelease);
        //Untyped
        public static IRXsObservableDisposable FromAction(Func<IRXsObservableDisposable<object>, IRXsObserverSubscription, IRXsSubscription> action, Action onRelease = null)
            => FromAction<object>(action, onRelease);
        public static IRXsObservableDisposable FromAction(Func<IRXsObserverSubscription, IRXsSubscription> action, Action onRelease = null)
            => FromAction((_, o) => action?.Invoke(o), onRelease);
    }
}