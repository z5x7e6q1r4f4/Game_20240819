using System;

namespace Main.RXs
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
                observable.Subscribe(operatorObserver);
                if (autoDispose) operatorObservable.Dispose();
                return operatorObserver;
            });
        public static IDisposable EnableDebug<T>(this IObservable<T> observable, string name = null)
            => observable.Debug(name).Subscribe(Observer.Create<T>());
    }
}