using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IObservable<T> Order<T>(this IObservable<T> observable, int order, bool autoDispose = true)
            => Observable.Create<T>((operatorObservable, observer) =>
            {
                if (observer is IObserverOrderable orderable) orderable.Order = order;
                if (autoDispose) operatorObservable.Dispose();
                return observable.Subscribe(observer);
            });
    }
}