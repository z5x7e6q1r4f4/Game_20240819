using System;

namespace Main.RXs
{
    partial class RXsOperatorExtension
    {
        private class WhereOperatorHandler<T> : RXsOperatorHandler<T, T>
        {
            private Predicate<T> Predicate { get; }
            public WhereOperatorHandler(System.IObservable<T> source, Predicate<T> predicate) : base(source)
                => Predicate = predicate;
            public override IDisposable Subscribe(System.IObserver<T> observer)
                => Source.Subscribe(new WhereOperator<T>(observer, Predicate));
        }
        private class WhereOperator<T> : RXsOperator<T, T>
        {
            private Predicate<T> Predicate { get; }
            protected override void OnNext(T value)
            { if (Predicate(value)) Result.OnNext(value); }
            public WhereOperator(System.IObserver<T> result, Predicate<T> predicate) :
                base(result)
                => Predicate = predicate;
        }
        public static IObservable<T> Where<T>(this System.IObservable<T> observable, Predicate<T> predicate)
            => new WhereOperatorHandler<T>(observable, predicate);
    }
}