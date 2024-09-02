using System;

namespace Main.RXs
{
    partial class RXsOperation
    {
        public static IRXsObservable<TResult> Select<TSource, TResult>(this IRXsObservable<TSource> observable, Func<TSource, TResult> selector, bool autoDispose = true)
            => RXsObservable.FromAction<TResult>((self, observer) =>
            {
                var sub = observable.Subscribe(e => observer.OnNext(selector(e)), onRelease: observer is IDisposable disposable ? disposable.Dispose : null);
                if (autoDispose) self.Dispose();
                return sub;
            });
    }
}