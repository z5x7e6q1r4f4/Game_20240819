using System;

namespace Main.RXs
{
    public interface IRXsObservable { IRXsDisposable Subscribe(IRXsObserver observer); }
    public interface IRXsObservable<out T> : IObservable<T>, IRXsObservable
    { IRXsDisposable Subscribe(IRXsObserver<T> observer); }
}