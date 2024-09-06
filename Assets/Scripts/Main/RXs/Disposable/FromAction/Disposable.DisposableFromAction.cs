using System;
using System.Collections.Generic;

namespace Main.RXs
{
    partial class Disposable
    {
        private sealed class DisposableFromAction :
            DisposableBase,
            IDisposableFromAction,
            IReuseable.IOnRelease,
            IReuseable.IOnGet
        {
            public Reuse.IPool Pool { get; set; }
            private static Reuse.IPool<DisposableFromAction> StaticPool
                => staticPool ??= Reuse.GetPool<DisposableFromAction>(releaseOnClear: () => staticPool = null);
            private static Reuse.IPool<DisposableFromAction> staticPool;
            public event Action<IDisposableFromAction> OnDisposeAction;
            public void OnGet() => hasDisposed = false;
            public void OnRelease()
            {
                base.Dispose();
                OnDisposeAction?.Invoke(this);
                OnDisposeAction = null;
            }
            protected override void Dispose() => this.ReleaseToReusePool();
            public static DisposableFromAction GetFromReusePool() => StaticPool.Get();
        }
    }
}