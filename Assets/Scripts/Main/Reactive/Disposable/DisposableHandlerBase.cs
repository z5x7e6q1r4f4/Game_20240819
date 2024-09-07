using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Main
{
    public abstract class DisposableHandlerBase : IDisposableHandler
    {
        private readonly List<IDisposable> disposables = new();
        protected bool hasDisposed = false;
        void IDisposableHandler.Add(IDisposable disposable)
        { if (!disposables.Contains(disposable) && disposable != this) disposables.Add(disposable); }
        void IDisposableHandler.Remove(IDisposable disposable, bool tryDispose)
        { if (disposables.Remove(disposable) && tryDispose && disposables.Count == 0) Dispose(); }
        public void Dispose() { if (!hasDisposed) { hasDisposed = true; OnDispose(); } }
        protected virtual void OnDispose()
        {
            using var disposables = this.disposables.ToReuseList();
            foreach (var subscription in disposables) subscription.Dispose();
            this.disposables.Clear();
        }
    }
}