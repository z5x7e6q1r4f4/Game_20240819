using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IObservable<object> Merge<T1, T2>(this IObservable<T1> observable1, IObservable<T2> observable2, bool autoDispose = true)
            => Observable.Create<object>((operatorObservable, observer) =>
            {
                if (autoDispose) operatorObservable.Dispose();
                return Disposable.Create(
                    observable1.OfType<T1, object>().Subscribe(observer),
                    observable2.OfType<T2, object>().Subscribe(observer));
            });
    }
}