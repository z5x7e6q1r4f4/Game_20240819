using System;

namespace Main.RXs
{
    public class RXsEventHandler<T> :
        RXsSubjectBase<T>,
        IRXsObservableImmediately<T>,
        IRXsEventHandler<T>
    {
        Action<IRXsObserver<T>> IRXsObservableImmediately<T>.ImmediatelyAction => immediatelyAction;
        private readonly Action<IRXsObserver<T>> immediatelyAction;
        public RXsEventHandler(Action<IObserver<T>> immediatelyAction = null) => this.immediatelyAction = immediatelyAction;
        public virtual void Invoke(T eventArgs) =>OnNext(eventArgs);
        public virtual void Clear() => Dispose();
    }
}