using System;

namespace Main.RXs
{
    public static partial class RXsObservable
    {
        public static IRXsSubscription SubscribeToTyped<T>(this IRXsObservable observable, IRXsObserver observer)
            => (observable as IRXsObservable<T>).Subscribe(observer.ToRXsObserverSubscription<T>());
        public static IRXsSubscription SubscribeToTyped<T>(this IRXsObservable observable, IObserver<T> observer)
            => (observable as IRXsObservable<T>).Subscribe(observer.ToRXsObserverSubscription());
    }
}