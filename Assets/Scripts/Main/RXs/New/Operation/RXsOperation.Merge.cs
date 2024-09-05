using System.Collections.Generic;

namespace Main.RXs
{
    partial class Operation
    {
        public static IRXsObservable<object> Merge(this IRXsObservable observable, bool autoDispose, params IRXsObservable[] merges)
            => RXsObservable.FromAction<object>((self, observer) =>
            {
                List<IRXsDisposable> subs = new() { observable.Subscribe(observer) };
                foreach (var observable in merges) subs.Add(observable.Subscribe(observer));
                var sub = RXsSubscription.FromList(subs);
                subs.Clear();
                if (autoDispose) self.Dispose();
                return sub;
            });
        public static IRXsObservable<object> Merge(this IRXsObservable observable, params IRXsObservable[] merges)
            => Merge(observable, true, merges);
    }
}