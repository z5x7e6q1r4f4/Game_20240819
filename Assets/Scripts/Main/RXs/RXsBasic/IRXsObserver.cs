using System;

namespace Main.RXs
{
    public interface IObserver
    {
        void OnNext(object value);
        void OnCompleted();
        void OnError(Exception error);
    }
    public interface IRXsObserver<in T> : IObserver<T>, IObserver, IDisposable { }
}