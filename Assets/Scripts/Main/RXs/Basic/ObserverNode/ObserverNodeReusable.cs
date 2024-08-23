namespace Main.RXs
{
    public class ObserverNodeReusable<T, TName> :
        ObserverNode<T>,
        IReuseable.IOnRelease
        where TName : ObserverNodeReusable<T, TName>
    {
        Reuse.IPool IReuseable.Pool { get; set; }
        protected static Reuse.IPool<TName> StaticPool
            => staticPool ??= Reuse.GetPool<TName>(releaseOnClear: () => staticPool = null);
        private static Reuse.IPool<TName> staticPool;
        void IReuseable.IOnRelease.OnRelease() => OnRelease();
        protected virtual void OnRelease() => this.DefaultDisposeObserverNode();
        protected override void Dispose() => this.ReleaseToReusePool();
        protected static TName GetFromReusePool(bool onGet = true) => StaticPool.Get(onGet);
        protected ObserverNodeReusable() { }
    }
}