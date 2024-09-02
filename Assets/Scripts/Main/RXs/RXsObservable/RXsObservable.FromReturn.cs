using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    partial class RXsObservable
    {
        public static IRXsObservableDisposable<T> FromReturn<T>(IEnumerable<T> items)
            => FromAction<T>(o => { foreach (var item in items) o.OnNext(item); return o; });
        public static IRXsObservableDisposable<T> FromReturn<T>(params T[] items) => FromReturn(items.AsEnumerable());
    }
}