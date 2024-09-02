using System;

namespace Main.RXs
{
    public static partial class RXsObserver
    {
        public static void OnNextToTyped<T>(this IRXsObserver observer, object value)
            => (observer as IObserver<T>).OnNext((T)value);
        public static void OnErrorToTyped<T>(this IRXsObserver observer, Exception error)
            => (observer as IObserver<T>).OnError(error);
        public static void OnCompletedToTyped<T>(this IRXsObserver observer)
            => (observer as IObserver<T>).OnCompleted();
    }
}