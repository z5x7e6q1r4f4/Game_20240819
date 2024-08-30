using System;

namespace Main.RXs
{
    public interface IRXsSubject<in TObserver, out TObservable> :
        IRXsObserver<TObserver>,
        IRXsObservable<TObservable>
    { }
    public interface IRXsSubject<T> : IRXsSubject<T, T> { }
}