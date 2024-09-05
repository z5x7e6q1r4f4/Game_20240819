namespace Main.RXs
{
    public abstract class RXsObserverBaseReusable<TName, T> :
        RXsObserverBase<T>,
        IReuseable
        where TName : RXsObserverBaseReusable<TName, T>
    {
        Reuse.IPool IReuseable.Pool { get; set; }
        private static Reuse.IPool<TName> StaticPool => staticPool ??= Reuse.GetPool<TName>(releaseOnClear: () => staticPool = null);
        private static Reuse.IPool<TName> staticPool;
        protected static TName GetFromReusePool() => StaticPool.Get();
        protected override void Dispose()
        {
            base.Dispose();
            this.ReleaseToReusePool();
        }

    }
}