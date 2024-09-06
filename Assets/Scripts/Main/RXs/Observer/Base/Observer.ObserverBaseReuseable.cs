namespace Main.RXs
{
    partial class Observer
    {
        public abstract class ObserverBaseReuseable<TName, T> : ObserverBase<T>, IReuseable.IOnRelease
            where TName : ObserverBaseReuseable<TName, T>
        {
            Reuse.IPool IReuseable.Pool { get; set; }
            private static Reuse.IPool<TName> StaticPool =>
                staticPool ??= Reuse.GetPool<TName>(releaseOnClear: () => staticPool = null);
            private static Reuse.IPool<TName> staticPool;
            void IReuseable.IOnRelease.OnRelease() => OnRelease();
            protected virtual void OnRelease() => base.Dispose();
            protected override void Dispose() => this.ReleaseToReusePool();
            protected static TName GetFromReusePool(bool onGet = true) => StaticPool.Get(onGet);
        }
    }
}