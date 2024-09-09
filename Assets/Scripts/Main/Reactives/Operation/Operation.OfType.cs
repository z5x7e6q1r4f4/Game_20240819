using System;

namespace Main
{
    partial class Operation
    {
        public static IObservable<TTo> OfType<TFrom, TTo>(this IObservable<TFrom> observable, bool autoDispose = true)
            => Observable.Create<TTo>((operatorObservable, observer) =>
            {
                var operatorObserver = Observer.Create<TFrom>(value =>
                {
                    try { observer.OnNext((TTo)(object)value); } catch { }
                }, observer.OnCompleted, observer.OnError);
                if (autoDispose) operatorObservable.Dispose();
                return observable.SubscribeOperator(operatorObserver.AsOperatorOf(observer));
            });

        public static IObservable<T> OfType<T>(this IObservable<object> observable, bool autoDispose = true)
            => observable.OfType<object, T>(autoDispose);
    }
}