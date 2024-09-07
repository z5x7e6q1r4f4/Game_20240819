using System;

namespace Main
{
    partial class Operation
    {
        public static IObservable<T> Skip<T>(this IObservable<T> observable, int count, bool autoDispose = true)
            => Observable.Create<T>((operatorObservable, observer) =>
            {
                var operatorObserver = Observer.Create<T>(value =>
                {
                    if (count <= 0) observer.OnNext(value); else count--;
                }, observer.OnCompleted, observer.OnError);
                operatorObserver.AsOperatorOf(observer);
                observable.SubscribeOperator(operatorObserver);
                if (autoDispose) operatorObservable.Dispose();
                return operatorObserver;
            });
    }
}