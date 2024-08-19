using System;

namespace Main
{
    partial class Reuse
    {
        public interface IPool<T> : IPool 
            where T : class, IReuseable
        {
            object IPool.Get(bool onGet) => Get(onGet);
            void IPool.Release(object item, bool onRelease) => Release((T)item, onRelease);
            new T Get(bool onGet = true);
            void Release(T item, bool onRelease = true);
        }
        public interface IPool : IDisposable
        {
            int AllCount { get; }
            int ActiveCount { get; }
            int InactiveCount { get; }
            object Key { get; }
            bool IsPrefab { get; }
            object Get(bool onGet = true);
            void Release(object item, bool onRelease = true);
        }
    }
}