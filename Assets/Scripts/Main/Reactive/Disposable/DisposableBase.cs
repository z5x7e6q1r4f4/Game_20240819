using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Main
{
    public abstract class DisposableBase : IDisposableHandler
    {
        private readonly List<IDisposable> disposables = new();
        protected bool hasDisposed = false;
        int IDisposableHandler.Count => disposables.Count;
        void IDisposableHandler.Add(IDisposable disposable)
        { if (!disposables.Contains(disposable) && disposable != this) disposables.Add(disposable); }

        bool IDisposableHandler.Remove(IDisposable disposable) => disposables.Remove(disposable);
        public void Dispose() { if (!hasDisposed) { hasDisposed = true; OnDispose(); } }
        protected virtual void OnDispose()
        {
            using var disposables = this.disposables.ToReuseList();
            foreach (var subscription in disposables) subscription.Dispose();
            this.disposables.Clear();
        }
    }
}