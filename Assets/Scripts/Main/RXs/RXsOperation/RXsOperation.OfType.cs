using System;

namespace Main.RXs
{
    partial class RXsOperation
    {
        public static IRXsObservable<T> OfType<T>(this IRXsObservable observable, bool autoDispose = true)
            => RXsObservable.FromAction<T>((self, observer) =>
            {
                var sub = observable.Subscribe(e =>
                {
                    if (e is T typed) { observer.OnNext(typed); return; }
                    if (Equals(e, default(T))) { observer.OnNext(default); return; }
                }, onDispose: observer.Dispose);
                if (autoDispose) self.Dispose();
                return sub;
            });
    }
}