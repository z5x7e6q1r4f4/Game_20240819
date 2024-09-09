using System;

namespace Main
{
    partial class Operation
    {
        public static IObservable<T> Order<T>(this IObservable<T> observable, int order, bool autoDispose = true)
            => Observable.Create<T>((operatorObservable, observer) =>
            {
                observer.Order = order;
                if (autoDispose) operatorObservable.Dispose();
                return observable.Subscribe(observer);
            });
    }
}