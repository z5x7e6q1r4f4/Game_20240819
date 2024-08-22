using System;

namespace Main.RXs
{
    partial class RXsOperatorExtension
    {
        private class OfTypeOperatorHandler<T> : RXsOperatorHandler<T>
        {
            public OfTypeOperatorHandler(IObservable source) : base(source) { }
            public override IDisposable Subscribe(System.IObserver<T> observer)
                => Source.Subscribe(new OfTypeOperator<T>(observer));
        }
        private class OfTypeOperator<T> : RXsOperator<T>
        {
            public OfTypeOperator(System.IObserver<T> result) : base(result) { }
            public override void OnNext(object value)
            { if (value is T typed) Result.OnNext(typed); }
        }
        public static IObservable<T> OfType<T>(this IObservable observable)
            => new OfTypeOperatorHandler<T>(observable);
    }
}