using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main
{
    public static partial class Reuse
    {
        private static readonly Dictionary<object, IPool> Pools = new();
        private static event Action ReleaseOnClear = null;
        //Normal
        public static IPool<T> GetPool<T>(T prefab = null, Action releaseOnClear = null)
            where T : class, IReuseable
        {
            CheckRegisterUnityLifeCycle();
            var key = prefab ?? typeof(T) as object;
            if (!Pools.TryGetValue(key, out var pool))
            {
                pool = new Pool<T>(prefab);
                Pools.Add(key, pool);
            }
            if (releaseOnClear != null) ReleaseOnClear += releaseOnClear;
            return pool as IPool<T>;
        }
        public static T Get<T>(T prefab = null, bool onGet = true)
            where T : class, IReuseable
            => GetPool(prefab).Get(onGet);
        //
        public static void ReleaseToReusePool<T>(this T item, bool onRelease = true)
            where T : class, IReuseable
            => item.Pool?.Release(item, onRelease);
        public static bool IsActive(IReuseable reuseable) => reuseable.Pool?.Active.Contains(reuseable) ?? false;
        public static bool IsInactive(IReuseable reuseable) => reuseable.Pool?.Inactive.Contains(reuseable) ?? false;
        public static bool IsInPool(IReuseable reuseable) => reuseable.Pool?.All.Contains(reuseable) ?? false;
        //LifeCycle
        public static void Clear()
        {
            var pools = Pools.Values.ToArray();
            foreach (var pool in pools) pool.Dispose();
            //Pools.Clear();
            ReleaseOnClear?.Invoke();
            ReleaseOnClear = null;
        }
        private static bool hasRegister = false;
        private static void CheckRegisterUnityLifeCycle()
        {
            if (hasRegister) return;
            Application.quitting += Clear;
            ReleaseOnClear += () => hasRegister = false;
            hasRegister = true;
        }
        public static void Log()
        {
            foreach (var pool in Pools.Values) Debug.Log(pool);
        }
    }
}