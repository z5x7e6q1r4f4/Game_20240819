using System;

namespace Main
{
    partial class Disposable
    {
        public static IDisposable Until<T>(this IDisposable disposable, IObservable<T> onNext, int order = int.MinValue)
        {
            var observer = Observer.Create<T>((observer, _) => {
                disposable.Dispose();
                observer.Dispose();
            });
            return onNext.Order(order).Take(1).Subscribe(observer);
        }
    }
}