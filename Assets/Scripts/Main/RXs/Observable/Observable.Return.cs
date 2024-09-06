using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    partial class Observable
    {
        public static IObservableDisposable<T> Return<T>(IEnumerable<T> items)
            => Create<T>(o => { foreach (var item in items) o.OnNext(item); return o; });
        public static IObservableDisposable<T> Return<T>(params T[] items) => Return(items.AsEnumerable());
    }
}