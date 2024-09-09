namespace Main
{
    public abstract class DisposableBaseReuseable<TName> :
        DisposableBase,
        IReuseable.IOnRelease
    where TName : DisposableBaseReuseable<TName>
    {
        Reuse.IPool IReuseable.Pool { get; set; }
        private static Reuse.IPool<TName> StaticPool =>
            staticPool ??= Reuse.GetPool<TName>(releaseOnClear: () => staticPool = null);
        private static Reuse.IPool<TName> staticPool;
        void IReuseable.IOnRelease.OnRelease() => OnRelease();
        protected virtual void OnRelease() => base.OnDispose();
        protected override void OnDispose() => this.ReleaseToReusePool();
        protected static TName GetFromReusePool(bool onGet = true)
        {
            var disposable = StaticPool.Get(onGet);
            disposable.hasDisposed = false;
            return disposable;
        }
    }
}