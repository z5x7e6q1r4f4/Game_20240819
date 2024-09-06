using System;

namespace Main.RXs
{
    public static partial class Observable
    {
        public interface IObservableDisposable<out T> : IObservable<T>, IDisposable { }
    }
}
