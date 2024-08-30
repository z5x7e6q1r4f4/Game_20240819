namespace Main.RXs
{
    public abstract class RXsObserverItemReusable<TName, T> :
        RXsObserverItem<T>,
        IReuseable.IOnRelease
        where TName : RXsObserverItemReusable<TName, T>
    {
        Reuse.IPool IReuseable.Pool { get; set; }
        protected static Reuse.IPool<TName> StaticPool => staticPool ??= Reuse.GetPool<TName>(releaseOnClear: () => staticPool = null);
        private static Reuse.IPool<TName> staticPool;
        protected static TName GetFromReusePool() => StaticPool.Get();
        protected override void Dispose() => this.ReleaseToReusePool();
        void IReuseable.IOnRelease.OnRelease() => OnRelease();
        protected virtual void OnRelease() => base.Dispose();
    }
}