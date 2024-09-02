using System;

namespace Main.RXs
{
    public interface IRXsObservable { IRXsSubscription Subscribe(IRXsObserver observer); }
    public interface IRXsObservable<out T> : IObservable<T>, IRXsObservable
    { IRXsSubscription Subscribe(IRXsObserver<T> observer); }
}