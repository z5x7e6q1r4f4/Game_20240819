using System;

namespace Main.RXs
{
    public static partial class RXsObserver
    {
        public static void OnNextToTyped<T>(this IObserver observer, object value)
            => (observer as IObserver<T>).OnNext((T)value);
        public static void OnErrorToTyped<T>(this IObserver observer, Exception error)
            => (observer as IObserver<T>).OnError(error);
        public static void OnCompletedToTyped<T>(this IObserver observer)
            => (observer as IObserver<T>).OnCompleted();
    }
}