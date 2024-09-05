using Mono.Cecil;
using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IRXsObservable<T> Where<T>(this IRXsObservable<T> observable, Predicate<T> predicate, bool autoDispose = true)
            => RXsObservable.FromAction<T>((self, observer) =>
            {
                var sub = observable.Subscribe(e => { if (predicate(e)) observer.OnNext(e); }, onDispose: observer is IDisposable disposable ? disposable.Dispose : null);
                if (autoDispose) self.Dispose();
                return sub;
            });
    }
}