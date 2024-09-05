using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IRXsObservable<T> Skip<T>(this IRXsObservable<T> observable, int count, bool autoDispose = true)
            => RXsObservable.FromAction<T>((self, observer) =>
            {
                var sub = observable.Subscribe(e => { if (count <= 0) observer.OnNext(e); else count--; }, onDispose: observer.Dispose);
                if (autoDispose) self.Dispose();
                return sub;
            });
        public static IRXsObservable Skip(this IRXsObservable observable, int count, bool autoDispose = true)
            => RXsObservable.FromAction((self, observer) =>
            {
                var sub = observable.Subscribe(e => { if (count <= 0) observer.OnNext(e); else count--; }, onDispose: observer.Dispose);
                if (autoDispose) self.Dispose();
                return sub;
            });
    }
}