namespace Main
{
    partial class Reuse
    {
        public class ObjectBase<T> : IReuseable
            where T : ObjectBase<T>
        {
            private static IPool<T> StaticPool => staticPool ??= GetPool<T>(releaseOnClear: () => staticPool = null);
            private static IPool<T> staticPool;
            IPool IReuseable.Pool { get; set; }
            protected static T GetFromReusePool(bool onGet) => StaticPool.Get(onGet);
        }
    }
}