using System;

namespace Main.RXs
{
    public interface IRXsObservableImmediately : IRXsObservable
    {
        Action<IRXsObserver> ImmediatelyAction { get; }
    }
    public interface IRXsObservableImmediately<out T> : IRXsObservable<T>, IRXsObservableImmediately
    {
        Action<IRXsObserver> IRXsObservableImmediately.ImmediatelyAction => ImmediatelyAction as Action<IRXsObserver>;
        new Action<IRXsObserver<T>> ImmediatelyAction { get; }
    }
}