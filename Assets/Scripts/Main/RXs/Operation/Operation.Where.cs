using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IObservable<T> Where<T>(this IObservable<T> observable, Predicate<T> predicate, bool autoDispose = true)
            => Observable.Create<T>((operatorObservable, observer) =>
            {
                var operatorObserver = Observer.Create<T>(value =>
                {
                    if (predicate(value)) { observer.OnNext(value); }
                }, observer.OnCompleted, observer.OnError);
                operatorObserver.AsOperatorOf(observer);
                observable.Subscribe(operatorObserver);
                if (autoDispose) operatorObservable.Dispose();
                return operatorObserver;
            });
    }
}