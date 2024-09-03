using System;

namespace Main.RXs
{
    partial class RXsOperation
    {
        public static IRXsObservable<T> Skip<T>(IRXsObservable<T> observable, int count, bool autoDispose)
            => RXsObservable.FromAction<T>((self, observer) =>
            {
                var sub = observable.Subscribe(e => { if (count <= 0) observer.OnNext(e); if (count > 0) count--; }, onDispose: observer.Dispose);
                if (autoDispose) self.Dispose();
                return sub;
            });
        public static IRXsObservable Skip(IRXsObservable observable, int count, bool autoDispose)
            => RXsObservable.FromAction((self, observer) =>
            {
                var sub = observable.Subscribe(e => { if (count <= 0) observer.OnNext(e); if (count > 0) count--; }, onDispose: observer.Dispose);
                if (autoDispose) self.Dispose();
                return sub;
            });
    }
}