using System;

namespace Main.RXs
{
    partial class RXsOperation
    {
        public static IRXsObservable<T> Debug<T>(this IRXsObservable<T> observable, string name = null, bool autoDispose = true)
            => RXsObservable.FromAction<T>((self, observer) =>
            {
                var sub = observable.Subscribe(e =>
                {
                    UnityEngine.Debug.Log($"{(name != null ? name + " " : string.Empty)}{e}");
                    observer.OnNext(e);
                }, onDispose: observer.Dispose);
                if (autoDispose) self.Dispose();
                return sub;
            });
        public static IRXsObservable Debug(this IRXsObservable observable, string name = null, bool autoDispose = true)
            => RXsObservable.FromAction((self, observer) =>
            {
                var sub = observable.Subscribe(e =>
                 {
                     UnityEngine.Debug.Log($"{(name != null ? name + " " : string.Empty)}{e}");
                     observer.OnNext(e);
                 }, onDispose: observer.Dispose);
                if (autoDispose) self.Dispose();
                return sub;
            });
        public static IRXsSubscription EnableDebug<T>(this IRXsObservable<T> observable, string name = null)
            => observable.Subscribe(e => UnityEngine.Debug.Log($"{(name != null ? name + " " : string.Empty)}{e}"));
    }
}