using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IObservable<T> Take<T>(this IObservable<T> observable, int count, bool autoDispose = true)
            => Observable.Create<T>((operatorObservable, observer) =>
            {
                var operatorObserver = Observer.Create<T>((@operator, value) =>
                {
                    if (count > 0) { observer.OnNext(value); count--; }
                    if (count <= 0)  @operator.Dispose(); 
                }, _ => observer.OnCompleted(), (_, error) => observer.OnError(error));
                operatorObserver.AsOperatorOf(observer);
                observable.Subscribe(operatorObserver);
                if (autoDispose) operatorObservable.Dispose();
                return operatorObserver;
            });
    }
}