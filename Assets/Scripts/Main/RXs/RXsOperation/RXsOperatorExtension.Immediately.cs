using System;

namespace Main.RXs
{
    partial class RXsOperation
    {
        private class ImmediatelyOperatorHandler<T> : RXsOperatorHandler<T>
        {
            private readonly Action<IObserver<T>> immediatelyAction;
            public ImmediatelyOperatorHandler(IObservable source) : base(source) => immediatelyAction = (source as IObservableImmediately<T>)?.ImmediatelyAction;
            public override IDisposable Subscribe(IObserver<T> observer)
            {
                immediatelyAction?.Invoke(observer);
                return Source.SubscribeToTyped(observer);
            }
        }
        public static IRXsObservable<T> Immediately<T>(this IRXsObservable<T> observable)
            => new ImmediatelyOperatorHandler<T>(observable);
    }
}