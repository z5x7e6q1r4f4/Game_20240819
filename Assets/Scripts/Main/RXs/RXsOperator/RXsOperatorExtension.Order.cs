using System;

namespace Main.RXs
{
    partial class RXsOperatorExtension
    {
        private class OrderOperatorHandler<T> : RXsOperatorHandler<T>
        {
            private readonly int order;
            public OrderOperatorHandler(IObservable source, int order) : base(source) => this.order = order;
            public override IDisposable Subscribe(System.IObserver<T> observer)
            {
                var subscription = observer.ToSubscription();
                subscription.Order = order;
                return Source.Subscribe(subscription);
            }
        }
        public static IObservable<T> Order<T>(this IObservable source, int order)
            => new OrderOperatorHandler<T>(source, order);
        public static IObservable<T> Order<T>(this IObservable<T> source, int order)
            => new OrderOperatorHandler<T>(source, order);
    }
}