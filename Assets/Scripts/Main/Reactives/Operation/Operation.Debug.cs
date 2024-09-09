using System;

namespace Main
{
    partial class Operation
    {
        public static IObservable<T> Debug<T>(this IObservable<T> observable, string name = null, bool autoDispose = true)
            => Observable.Create<T>((operatorObservable, observer) =>
            {
                var operatorObserver = Observer.Create<T>(value =>
                {
                    UnityEngine.Debug.Log($"{(name != null ? name + " " : string.Empty)}{value}");
                    observer.OnNext(value);
                }, observer.OnCompleted, observer.OnError);
                operatorObserver.AsOperatorOf(observer);
                if (autoDispose) operatorObservable.Dispose();
                return observable.SubscribeOperator(operatorObserver); ;
            });
        public static IDisposable EnableDebug<T>(this IObservable<T> observable, string name = null, int order = int.MinValue)
            => observable.Order(order).Debug(name).Subscribe(Observer.Create<T>());
    }
}