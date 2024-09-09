using System;

namespace Main
{
    public abstract class DisposableBase : IDisposable, IDisposableBase
    {
        protected bool hasDisposed = false;

        public virtual event Action<IDisposableBase> OnDisposeAction;
        public void Dispose() { if (!hasDisposed) { hasDisposed = true; OnDispose(); } }
        protected virtual void OnDispose()
        {
            OnDisposeAction?.Invoke(this);
            OnDisposeAction = null;
        }
    }
}