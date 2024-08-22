using Mono.Cecil;
using System;

namespace Main.RXs
{
    public static partial class RXsOperatorExtension
    {
        private abstract class RXsOperatorHandler<TSource, TResult> : IObservable<TResult>
        {
            protected System.IObservable<TSource> Source { get; }
            public RXsOperatorHandler(IObservable<TSource> source) => Source = source;
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
        private abstract class RXsOperator<TSource, TResult> : ObserverNode<TSource>
        {
            protected System.IObserver<TResult> Result { get; }
            public override void OnCompleted() { Result.OnCompleted(); base.OnCompleted(); }
            public override void OnError(Exception error) { Result.OnError(error); base.OnError(error); }
            public RXsOperator(System.IObserver<TResult> result) => Result = result;
        }
        private abstract class RXsOperator<TResult> : IObserver
        {
            protected System.IObserver<TResult> Result { get; }
            public virtual void OnCompleted() => Result.OnCompleted();
            public virtual void OnError(Exception error) => Result.OnError(error);
            public abstract void OnNext(object value);
            public RXsOperator(System.IObserver<TResult> result) => Result = result;
        }
    }
}