using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IRXsObservable<T> Immediately<T>(this IRXsObservable<T> observable, bool autoDispose = true)
            => RXsObservable.FromAction<T>((self, observer) =>
            {
                (observable as IRXsObservableImmediately<T>)?.ImmediatelyAction?.Invoke(observer);
                if (autoDispose) self.Dispose();
                return observable.Subscribe(observer);
            });
        public static IRXsObservable Immediately(this IRXsObservable observable, bool autoDispose = true)
            => RXsObservable.FromAction((self, observer) =>
            {
                (observable as IRXsObservableImmediately)?.ImmediatelyAction?.Invoke(observer);
                if (autoDispose) self.Dispose();
                return observable.Subscribe(observer);
            });
    }
}