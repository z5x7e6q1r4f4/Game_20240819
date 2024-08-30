using System;

namespace Main.RXs
{
    public static partial class RXsOperatorExtension
    {
        private abstract class RXsOperatorHandler<TSource, TResult> : IObservable<TResult>
        {
            protected System.IObservable<TSource> Source { get; }
            public RXsOperatorHandler(System.IObservable<TSource> source) => Source = source;
            public abstract IDisposable Subscribe(System.IObserver<TResult> observer);
            IDisposable IObservable.Subscribe(IObserver observer) => this.SubscribeToTyped<TResult>(observer);
        }
        private abstract class RXsOperatorHandler<TResult> : IObservable<TResult>
        {
            protected IObservable Source { get; }
            public RXsOperatorHandler(IObservable source) => Source = source;
            public abstract IDisposable Subscribe(System.IObserver<TResult> observer);
            IDisposable IObservable.Subscribe(IObserver observer) => this.SubscribeToTyped<TResult>(observer);
        }
        private abstract class RXsOperator<TSource, TResult> : ObserverListSubscription<TSource>
        {
            protected System.IObserver<TResult> Result { get; }
            protected override void OnCompleted() { Result.OnCompleted(); }
            protected override void OnError(Exception error) { Result.OnError(error); }
            public RXsOperator(System.IObserver<TResult> result) => Result = result;
            protected override void Dispose()
            {
                (Result as IDisposable)?.Dispose();
                base.Dispose();
            }
        }
        private abstract class RXsOperator<TResult> : IObserver, IDisposable
        {
            protected System.IObserver<TResult> Result { get; }
            public virtual void OnCompleted() => Result.OnCompleted();
            public virtual void OnError(Exception error) => Result.OnError(error);
            public abstract void OnNext(object value);
            void IDisposable.Dispose() => Dispose();
            protected virtual void Dispose() => (Result as IDisposable)?.Dispose();
            public RXsOperator(System.IObserver<TResult> result) => Result = result;

        }
    }
}