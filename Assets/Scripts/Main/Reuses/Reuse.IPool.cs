using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    partial class Reuse
    {
        public interface IPool<T> : IPool
            where T : class, IReuseable
        {
            object IPool.Get(bool onGet) => Get(onGet);
            void IPool.Release(object item, bool onRelease) => Release((T)item, onRelease);
            IEnumerable<object> IPool.Active => Active;
            IEnumerable<object> IPool.Inactive => Inactive;
            IEnumerable<object> IPool.All => All;
            //
            new T Get(bool onGet = true);
            void Release(T item, bool onRelease = true);
            new IEnumerable<T> Active { get; }
            new IEnumerable<T> Inactive { get; }
            new IEnumerable<T> All => Active.Concat(Inactive);
        }
        public interface IPool : IDisposable
        {
            object Key { get; }
            bool IsPrefab { get; }
            object Get(bool onGet = true);
            void Release(object item, bool onRelease = true);
            IEnumerable<object> Active { get; }
            IEnumerable<object> Inactive { get; }
            IEnumerable<object> All { get; }
        }
    }
}