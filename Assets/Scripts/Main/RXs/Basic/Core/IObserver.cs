using System;

namespace Main.RXs
{
    public interface IObserver
    {
        void OnNext(object value);
        void OnCompleted();
        void OnError(Exception error);
    }
    public interface IObserver<in T> : System.IObserver<T>, IObserver { }
    public static partial class Observer
    {
        public static void OnNextToTyped<T>(this IObserver observer, object value)
            => (observer as System.IObserver<T>).OnNext((T)value);
        public static void OnErrorToTyped<T>(this IObserver observer, Exception error)
            => (observer as System.IObserver<T>).OnError(error);
        public static void OnCompletedToTyped<T>(this IObserver observer)
            => (observer as System.IObserver<T>).OnCompleted();
    }
}