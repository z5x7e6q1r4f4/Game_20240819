using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    partial class Observable
    {
        public static IObservable<T> Return<T>(IEnumerable<T> items) 
        {
            return new ObservableFromAction<T>(o =>
            {
                foreach (var item in items) o.OnNext(item);
            });
        }
        public static IObservable<T> Return<T>(params T[] items)=>Return(items.AsEnumerable());
        public static IObservable<int> Range(int from, int to)
        {
            var dir = (to - from) < 0 ? -1 : 1;
            return new ObservableFromAction<int>(o =>
            {
                for (int i = from; i != to; i += dir) o.OnNext(i);
            });
        }
    }
}