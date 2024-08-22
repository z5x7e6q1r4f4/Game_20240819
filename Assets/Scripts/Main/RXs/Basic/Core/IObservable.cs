using System;

namespace Main.RXs
{
    public interface IObservable { IDisposable Subscribe(IObserver observer); }
    public interface IObservable<out T> : System.IObservable<T>, IObservable { }
    public static partial class Observable
    {
        public static IDisposable SubscribeToTyped<T>(this IObservable observable, IObserver observer)
          => (observable as System.IObservable<T>).Subscribe(observer.ToTyped<T>());
    }
}