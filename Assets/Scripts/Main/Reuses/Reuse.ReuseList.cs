using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    partial class Reuse
    {
        public sealed class ReuseList<T> : List<T>, IReuseable.IOnRelease, IDisposable
        {
            private static IPool<ReuseList<T>> StaticPool => staticPool ??= GetPool<ReuseList<T>>(releaseOnClear: () => staticPool = null);
            private static IPool<ReuseList<T>> staticPool;
            IPool IReuseable.Pool { get; set; }
            void IReuseable.IOnRelease.OnRelease() => Clear();
            public static ReuseList<T> Get(IEnumerable<T> collection)
            {
                var list = StaticPool.Get(false);
                list.AddRange(collection);
                return list;
            }
            public static ReuseList<T> Get(params T[] collection) => Get(collection.AsEnumerable());
            void IDisposable.Dispose() => this.ReleaseToReusePool();
        }
        public static ReuseList<T> ToReuseList<T>(this IEnumerable<T> collection) => ReuseList<T>.Get(collection);
    }
}