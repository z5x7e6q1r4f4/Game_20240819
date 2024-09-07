using System;

namespace Main
{
    partial class Disposable
    {
        public sealed class DisposableFromAction :
            DisposableBase,
            IReuseable.IOnRelease,
            IReuseable.IOnGet
        {
            Reuse.IPool IReuseable.Pool { get; set; }
            private static Reuse.IPool<DisposableFromAction> StaticPool
                => staticPool ??= Reuse.GetPool<DisposableFromAction>(releaseOnClear: () => staticPool = null);
            private static Reuse.IPool<DisposableFromAction> staticPool;
            public event Action<DisposableFromAction> OnDisposeAction;
            void IReuseable.IOnGet.OnGet() => hasDisposed = false;
            void IReuseable.IOnRelease.OnRelease()
            {
                base.Dispose();
                OnDisposeAction?.Invoke(this);
                OnDisposeAction = null;
            }
            public override void Dispose() => this.ReleaseToReusePool();
            public static DisposableFromAction GetFromReusePool() => StaticPool.Get();
        }
    }
}