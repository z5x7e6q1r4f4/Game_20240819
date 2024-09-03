using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public abstract class RXsSubjectBase<TObserver, TObservable> :
        RXsObserverBase<TObserver>,
        IRXsSubject<TObserver, TObservable>
    {
        protected List<IRXsObserverSubscription<TObservable>> Observers = new();
        protected override void OnCompleted()
        { foreach (var observer in Observers) observer.OnCompletedToTyped<TObservable>(); }
        protected override void OnError(Exception error)
        { foreach (var observer in Observers) observer.OnErrorToTyped<TObservable>(error); }
        IRXsSubscription IRXsObservable<TObservable>.Subscribe(IRXsObserver<TObservable> observer) => Subscribe(observer.ToRXsObserverSubscription());
        IDisposable IObservable<TObservable>.Subscribe(IObserver<TObservable> observer) => Subscribe(observer.ToRXsObserverSubscription());
        IRXsSubscription IRXsObservable.Subscribe(IRXsObserver observer) => Subscribe(observer.ToRXsObserverSubscription<TObservable>());
        protected virtual IRXsSubscription Subscribe(IRXsObserverSubscription<TObservable> observer)
        {
            Observers.Add(observer);
            observer.AddSubscription(RXsSubscription.FromAction(
                () => { if (!Observers.Contains(observer)) Observers.Add(observer); },
                () => Observers.Remove(observer)));
            return observer;
        }
        protected override void Dispose()
        {
            while (0 < Observers.Count)
            { Observers[0].Dispose(); }
            Observers.Clear();
            base.Dispose();
        }
    }
    public abstract class RXsSubjectBase<T> : RXsSubjectBase<T, T>, IRXsSubject<T>
    {
        protected override void OnNext(T value)
        { foreach (var observer in Observers) observer.OnNextToTyped<T>(value); }
    }
}