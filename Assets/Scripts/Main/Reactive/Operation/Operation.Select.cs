using System;

namespace Main
{
    partial class Operation
    {
        public static IObservable<TResult> Select<TSource, TResult>(this IObservable<TSource> observable, Func<TSource, TResult> selector, bool autoDispose = true)
            => Observable.Create<TResult>((operatorObservable, observer) =>
            {
                var operatorObserver = Observer.Create<TSource>(value =>
                {
                    observer.OnNext(selector(value));
                }, observer.OnCompleted, observer.OnError);
                if (autoDispose) operatorObservable.Dispose();
                return observable.SubscribeOperator(operatorObserver.AsOperatorOf(observer));
            });
    }
}