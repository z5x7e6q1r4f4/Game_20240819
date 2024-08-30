using System;

namespace Main.RXs
{
    partial class RXsOperation
    {
        private class SelectOperatorHandler<TSource, TResult> : RXsOperatorHandler<TSource, TResult>
        {
            private Func<TSource, TResult> Selector { get; }
            public SelectOperatorHandler(IObservable<TSource> source, Func<TSource, TResult> selector) :
                base(source)
                => Selector = selector;
            public override IDisposable Subscribe(IObserver<TResult> observer)
                => Source.Subscribe(new SelectOperator<TSource, TResult>(observer, Selector));
        }
        private class SelectOperator<TSource, TResult> : RXsOperator<TSource, TResult>
        {
            private Func<TSource, TResult> Selector { get; }
            public SelectOperator(IObserver<TResult> result, Func<TSource, TResult> selector) :
                base(result)
                => Selector = selector;
            protected override void OnNext(TSource value) => Result.OnNext(Selector(value));
        }
        public static IRXsObservable<TResult> Select<TSource, TResult>(this IObservable<TSource> observable, Func<TSource, TResult> selector)
            => new SelectOperatorHandler<TSource, TResult>(observable, selector);
    }
}