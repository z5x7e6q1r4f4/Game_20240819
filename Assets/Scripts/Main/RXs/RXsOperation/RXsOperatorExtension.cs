using Mono.Cecil;
using System;

namespace Main.RXs
{
    public static partial class RXsOperation
    {
        private abstract class RXsOperatorHandler<TName, TSource, TResult> :
            Reuse.ObjectBase<TName>,
            IRXsObservable<TResult>,
            IReuseable.IOnRelease
            where TName : RXsOperatorHandler<TName, TSource, TResult>
        {
            protected IObservable<TSource> Source { get; }
            public RXsOperatorHandler(IObservable<TSource> source) => Source = source;
            public abstract IDisposable Subscribe(IObserver<TResult> observer);
            IDisposable IObservable.Subscribe(IObserver observer) => this.SubscribeToTyped<TResult>(observer);

            void IReuseable.IOnRelease.OnRelease()
            {
                throw new NotImplementedException();
            }
            protected virtual void OnRelease
        }
        private abstract class RXsOperatorHandler<TName, TResult> :
            Reuse.ObjectBase<TName>,
            IRXsObservable<TResult>,
            IReuseable.IOnRelease
            where TName : RXsOperatorHandler<TName, TResult>
        {
            protected IObservable Source { get; }
            public RXsOperatorHandler(IObservable source) => Source = source;
            public abstract IDisposable Subscribe(IObserver<TResult> observer);
            IDisposable IObservable.Subscribe(IObserver observer) => this.SubscribeToTyped<TResult>(observer);
        }
        private abstract class RXsOperator<TSource, TResult> : RXsObserverItem<TSource>
        {
            protected IObserver<TResult> Result { get; }
            protected override void OnCompleted() { Result.OnCompleted(); }
            protected override void OnError(Exception error) { Result.OnError(error); }
            public RXsOperator(IObserver<TResult> result) => Result = result;
            protected override void Dispose()
            {
                (Result as IDisposable)?.Dispose();
                base.Dispose();
            }
        }
        private abstract class RXsOperator<TResult> : IObserver, IDisposable
        {
            protected IObserver<TResult> Result { get; }
            public virtual void OnCompleted() => Result.OnCompleted();
            public virtual void OnError(Exception error) => Result.OnError(error);
            public abstract void OnNext(object value);
            void IDisposable.Dispose() => Dispose();
            protected virtual void Dispose() => (Result as IDisposable)?.Dispose();
            public RXsOperator(IObserver<TResult> result) => Result = result;

        }
    }
}