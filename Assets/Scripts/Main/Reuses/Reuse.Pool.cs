using System.Collections.Generic;

namespace Main
{
    public static partial class Reuse
    {
        private class Pool<T> : IPool<T>
            where T : class, IReuseable
        {
            public object Key { get; }
            public bool IsPrefab { get; }
            IEnumerable<T> IPool<T>.Active => active;
            IEnumerable<T> IPool<T>.Inactive => inactive;
            private readonly ReuseList<T> active = new();
            private readonly ReuseList<T> inactive = new();
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
                //ReusePools
                Pools.Remove(Key);
            }
            private void Destroy(T item)
            {
                item.Pool = null;
                if (item is IReuseable.IOnDestroy destroy) destroy.OnDestroy();
            }
            public override string ToString()
            {
                var type = typeof(T);
                var prefab = Prefab?.ToString() ?? "Null";
                return $"Pool<<color=yellow>{type}</color>>," +
                    $"Prefab=<color=yellow>{prefab}</color>," +
                    $"All=<color=green>{active.Count + inactive.Count}</color>," +
                    $"Active={(active.Count == 0 ? $"<color=green>0" : $"<color=red>{active.Count}")}</color>," +
                    $"Inactive=<color=green>{inactive.Count}</color>";
            }
        }
    }
}