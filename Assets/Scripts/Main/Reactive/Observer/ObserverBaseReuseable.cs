﻿namespace Main
{
    public abstract class ObserverBaseReuseable<TName, T> : ObserverBase<T>, IReuseable.IOnRelease
        where TName : ObserverBaseReuseable<TName, T>
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
            var observer = StaticPool.Get(onGet);
            observer.hasDisposed = false;
            return observer;
        }
    }
}