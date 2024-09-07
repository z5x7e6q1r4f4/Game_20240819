using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Main
{
    public abstract class DisposableBase : IDisposableContainer
    {
        private readonly List<IDisposable> disposables = new();
        protected bool hasDisposed = false;
        int IDisposableContainer.Count => disposables.Count;
        void IDisposableContainer.Add(IDisposable disposable) => disposables.Add(disposable);
        bool IDisposableContainer.Remove(IDisposable disposable) => disposables.Remove(disposable);
        public virtual void Dispose()
        {
            Assert.IsFalse(hasDisposed);
            hasDisposed = true;
            using var disposables = this.disposables.ToReuseList();
            foreach (var subscription in disposables) subscription.Dispose();
            this.disposables.Clear();
        }
    }
}