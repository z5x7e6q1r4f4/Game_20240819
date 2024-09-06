using System;

namespace Main.RXs
{
    partial class Disposable
    {
        public static IDisposable Until<T>(this IDisposable disposable, IObservable<T> onNext, int order = int.MinValue)
            => onNext.Order(order).Take(1).Subscribe(disposable.Dispose);
    }
}