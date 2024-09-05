using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IObservable<T> Debug<T>(this IObservable<T> observable, string name = null, bool autoDispose = true)
            => Observables.Create<T>((self, observer) =>
            {
                var @operator = Observers.Create<T>(e =>
                {
                    UnityEngine.Debug.Log($"{(name != null ? name + " " : string.Empty)}{e}");
                    observer.OnNext(e);
                });
                @operator.AsOperatorOf(observer);
                if (autoDispose) self.Dispose();
                observable.Subscribe(@operator);
                return @operator;
            });
        public static IDisposable EnableDebug<T>(this IRXsObservable<T> observable, string name = null)
            => observable.Debug(name).Subscribe(Observers.Create<T>());
    }
}