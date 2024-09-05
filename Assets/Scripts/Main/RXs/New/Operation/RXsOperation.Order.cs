using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IRXsObservable<T> Order<T>(this IRXsObservable<T> observable, int order, bool autoDispose = true)
            => RXsObservable.FromAction<T>((self, observer) =>
            {
                if (observer is IObserverOrderable orderable) orderable.Order = order;
                if (autoDispose) self.Dispose();
                return observable.Subscribe(observer);
            });
        public static IRXsObservable Order(this IRXsObservable observable, int order, bool autoDispose = true)
            => RXsObservable.FromAction((self, observer) =>
            {
                if (observer is IObserverOrderable orderable) orderable.Order = order;
                if (autoDispose) self.Dispose();
                return observable.Subscribe(observer);
            });
    }
}