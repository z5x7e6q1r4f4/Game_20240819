using System.Collections.Generic;
using System.Linq;

namespace Main
{
    partial class Observable
    {
        public static ObservableFromAction<T> Return<T>(IEnumerable<T> items)
            => Create<T>(o => { foreach (var item in items) o.OnNext(item); return o; });
        public static ObservableFromAction<T> Return<T>(params T[] items) => Return(items.AsEnumerable());
    }
}