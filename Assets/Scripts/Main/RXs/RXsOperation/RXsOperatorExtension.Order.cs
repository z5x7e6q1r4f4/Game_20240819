using System;

namespace Main.RXs
{
    partial class RXsOperation
    {
        private class OrderOperatorHandler<T> : RXsOperatorHandler<T>
        {
            private readonly int order;
            public OrderOperatorHandler(IObservable source, int order) : base(source) => this.order = order;
            public override IDisposable Subscribe(IObserver<T> observer)
            {
                var subscription = observer.ToObserverItem();
                subscription.Order = order;
                return Source.Subscribe(subscription);
            }
        }
        public static IRXsObservable<T> Order<T>(this IObservable source, int order)
            => new OrderOperatorHandler<T>(source, order);
        public static IRXsObservable<T> Order<T>(this IRXsObservable<T> source, int order)
            => new OrderOperatorHandler<T>(source, order);
    }
}