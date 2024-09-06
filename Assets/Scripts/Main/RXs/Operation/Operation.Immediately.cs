using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IObservable<T> Immediately<T>(this IObservableImmediately<T> observable, bool autoDispose = true)
            => Observable.Create<T>((operatorObservable, observer) =>
            {
                observable.ImmediatelyAction?.Invoke(observer);
                if (autoDispose) operatorObservable.Dispose();
                return observable.Subscribe(observer);
            });
    }
}