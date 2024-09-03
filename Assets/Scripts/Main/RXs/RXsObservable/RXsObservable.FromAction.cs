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
            => FromAction<T>((_, observer) => action(observer), onRelease);
        public static IRXsObservableDisposable<T> FromAction<T>(Action<IRXsObservableDisposable<T>, IRXsObserverSubscription<T>> action, Action onRelease = null)
            => FromAction<T>((self, observer) => { action(self, observer); return observer; }, onRelease);
        public static IRXsObservableDisposable<T> FromAction<T>(Action<IRXsObserverSubscription<T>> action, Action onRelease = null)
            => FromAction<T>((_, observer) => { action(observer); return observer; }, onRelease);
        //Untyped
        public static IRXsObservableDisposable FromAction(Func<IRXsObservableDisposable, IRXsObserverSubscription, IRXsSubscription> action, Action onRelease = null)
            => FromAction<object>(action, onRelease);
        public static IRXsObservableDisposable FromAction(Func<IRXsObserverSubscription, IRXsSubscription> action, Action onRelease = null)
            => FromAction((_, observer) => action?.Invoke(observer), onRelease);
        public static IRXsObservableDisposable FromAction(Action<IRXsObservableDisposable, IRXsObserverSubscription> action, Action onRelease = null)
            => FromAction((self, observer) => { action(self, observer); return observer; }, onRelease);
        public static IRXsObservableDisposable FromAction(Action<IRXsObserverSubscription> action, Action onRelease = null)
            => FromAction((_, observer) => { action(observer); return observer; }, onRelease);
    }
}