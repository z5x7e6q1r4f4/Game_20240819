using System;

namespace Main.RXs
{
    partial class RXsObserver
    {
        public static IRXsObserverSubscription<T> ToRXsObserverSubscription<T>(this IObserver<T> observer)
            => observer as IRXsObserverSubscription<T> ??
            FromAction<T>(observer.OnNext, observer.OnCompleted, observer.OnError, (observer is IDisposable disposable) ? disposable.Dispose : null).ToRXsObserverSubscription();
        public static IRXsObserverSubscription<T> ToRXsObserverSubscription<T>(this IRXsObserver observer)
            => observer as IRXsObserverSubscription<T> ??
            FromAction<T>(value => observer.OnNext(value), observer.OnCompleted, observer.OnError, (observer is IDisposable disposable) ? disposable.Dispose : null).ToRXsObserverSubscription();
        public static IRXsObserverSubscription<T> ToRXsObserverSubscription<T>(this IRXsObserver<T> observer)
            => observer as IRXsObserverSubscription<T> ??
            FromAction<T>(observer.OnNext, observer.OnCompletedToTyped<T>, observer.OnErrorToTyped<T>, (observer is IDisposable disposable) ? disposable.Dispose : null).ToRXsObserverSubscription();
    }
}