using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IRXsObservable<T> Take<T>(this IRXsObservable<T> observable, int count, bool autoDispose = true)
            => RXsObservable.FromAction<T>((self, observer) =>
            {
                var sub = observable.Subscribe((obs, e) =>
                {
                    if (count > 0) { observer.OnNext(e); count--; }
                    if (count <= 0) { obs.Dispose(); if (observer is IDisposable disposable) disposable.Dispose(); }
                }, onDispose: observer is IDisposable disposable ? disposable.Dispose : null);
                if (autoDispose) self.Dispose();
                return sub;
            });
        public static IRXsObservable Take(this IRXsObservable observable, int count, bool autoDispose = true)
            => RXsObservable.FromAction((self, observer) =>
            {
                var sub = observable.Subscribe((obs, e) =>
                {
                    if (count > 0) { observer.OnNext(e); count--; }
                    if (count <= 0) { obs.Dispose(); if (observer is IDisposable disposable) disposable.Dispose(); }
                }, onDispose: observer is IDisposable disposable ? disposable.Dispose : null);
                if (autoDispose) self.Dispose();
                return sub;
            });
    }
}