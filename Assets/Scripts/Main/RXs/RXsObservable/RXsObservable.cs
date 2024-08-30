using System;

namespace Main.RXs
{
    public static partial class RXsObservable
    {
        public static IDisposable SubscribeToTyped<T>(this IObservable observable, IObserver observer)
            => (observable as IObservable<T>).Subscribe(observer.ToTyped<T>());
        public static IDisposable SubscribeToTyped<T>(this IObservable observable, IObserver<T> observer)
            => (observable as IObservable<T>).Subscribe(observer);
    }
}