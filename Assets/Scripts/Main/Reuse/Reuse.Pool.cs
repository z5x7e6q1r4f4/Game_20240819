﻿using System.Collections.Generic;
using static PlasticPipe.PlasticProtocol.Messages.Serialization.ItemHandlerMessagesSerialization;

namespace Main
{
    public static partial class Reuse
    {
        private class Pool<T> : IPool<T>
            where T : class, IReuseable
        {
            public int AllCount => ActiveCount + InactiveCount;
            public int ActiveCount => active.Count;
            public int InactiveCount => inactive.Count;
            public object Key { get; }
            public bool IsPrefab { get; }
            private readonly List<T> active = new();
            private readonly List<T> inactive = new();
            private T Prefab { get; }
            public Pool(T prefab)
            {
                Prefab = prefab;
                IsPrefab = prefab != null;
                Key = prefab ?? typeof(T) as object;
            }
            public T Get(bool onGet = true)
            {
                CheckCountAndGenerate();
                var item = inactive[0];
                inactive.RemoveAt(0);
                active.Add(item);
                if (onGet && item is IReuseable.IOnGet _onGet) _onGet.OnGet();
                return item;
            }
            public void Release(T item, bool onRelease = true)
            {
                if (inactive.Contains(item)) return;//has released
                if (onRelease && item is IReuseable.IOnRelease _onRelease) _onRelease.OnRelease();
                if (active.Contains(item))//normal
                {
                    active.Remove(item);
                    inactive.Add(item);
                }
                else Destroy(item);//has disposed
            }
            private void CheckCountAndGenerate()
            {
                if (inactive.Count > 0) return;
                T item;
                if (IsPrefab) item = Objects.Clone(Prefab);
                else item = Objects.New<T>();
                item.Pool = this;
                inactive.Add(item);
            }
            public void Dispose()
            {
                //Inactive
                foreach (var item in inactive) Destroy(item);
                inactive.Clear();
                //Active
                active.Clear();
            }
            private void Destroy(T item)
            {
                item.Pool = null;
                if (item is IReuseable.IOnDestroy destroy) destroy.OnDestroy();
            }
        }
    }
}