using System;

namespace Main.RXs
{
    partial class RXsOperatorExtension
    {
        private class ImmediatelyOperatorHandler<T> : RXsOperatorHandler<T>
        {
            private readonly Action<System.IObserver<T>> immediatelyAction;
            public ImmediatelyOperatorHandler(IObservable source) : base(source) => immediatelyAction = (source as IObservableImmediately<T>)?.ImmediatelyAction;
            public override IDisposable Subscribe(System.IObserver<T> observer)
            {
                immediatelyAction?.Invoke(observer);
                return Source.SubscribeToTyped<T>(observer);
            }
        }
        public static IObservable<T> Immediately<T>(this IObservable<T> observable)
            => new ImmediatelyOperatorHandler<T>(observable);
    }
}