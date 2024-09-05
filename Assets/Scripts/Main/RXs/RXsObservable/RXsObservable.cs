using System;

namespace Main.RXs
{
    public static partial class RXsObservable
    {
        public static IRXsDisposable SubscribeToTyped<T>(this IRXsObservable observable, IRXsObserver observer)
            => (observable as IRXsObservable<T>).Subscribe(observer.ToRXsObserverSubscription<T>());
        public static IRXsDisposable SubscribeToTyped<T>(this IRXsObservable observable, IObserver<T> observer)
            => (observable as IRXsObservable<T>).Subscribe(observer.ToRXsObserverSubscription());
    }
}