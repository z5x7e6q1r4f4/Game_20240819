using System;

namespace Main.RXs
{
    public static partial class Disposables
    {
        private sealed class DisposableFromAction :
            Reuse.ObjectBase<DisposableFromAction>,
            IDisposableFromAction,
            IReuseable.IOnRelease
        {
            public event Action<IDisposableFromAction> OnDisposeAction;
            public void Dispose() => this.ReleaseToReusePool();
            void IReuseable.IOnRelease.OnRelease()
            {
                OnDisposeAction?.Invoke(this);
                OnDisposeAction = null;
            }
            public static DisposableFromAction GetFromReusePool() => GetFromReusePool(false);
        }
    }
}