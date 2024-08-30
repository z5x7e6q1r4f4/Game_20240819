using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    partial class RXsObservable
    {
        public static IRXsObservable<T> FromReturn<T>(IEnumerable<T> items) 
            => FromAction<T>(o => { foreach (var item in items) o.OnNext(item); });
        public static IRXsObservable<T> FromReturn<T>(params T[] items) => FromReturn(items.AsEnumerable());
    }
}