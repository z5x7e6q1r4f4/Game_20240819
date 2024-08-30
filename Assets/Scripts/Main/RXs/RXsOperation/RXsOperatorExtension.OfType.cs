using System;

namespace Main.RXs
{
    partial class RXsOperation
    {
        private class OfTypeOperatorHandler<T> : RXsOperatorHandler<T>
        {
            public OfTypeOperatorHandler(IObservable source) : base(source) { }
            public override IDisposable Subscribe(IObserver<T> observer)
                => Source.Subscribe(new OfTypeOperator<T>(observer));
        }
        private class OfTypeOperator<T> : RXsOperator<T>
        {
            public OfTypeOperator(IObserver<T> result) : base(result) { }
            public override void OnNext(object value)
            { if (value is T typed) Result.OnNext(typed); }
        }
        public static IRXsObservable<T> OfType<T>(this IObservable observable)
            => new OfTypeOperatorHandler<T>(observable);
    }
}