using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    public static partial class Disposable
    {
        public static DisposableFromAction Create(Action<DisposableFromAction> onDispose = null)
        {
            var disposable = DisposableFromAction.GetFromReusePool();
            disposable.OnDisposeAction += onDispose;
            return disposable;
        }
        public static DisposableFromAction Create(IEnumerable<IDisposable> disposables)
            => Create(_ => { foreach (var disposable in disposables) disposable.Dispose(); });
        public static DisposableFromAction Create(params IDisposable[] disposables)
            => Create(disposables.AsEnumerable());
        public static DisposableFromAction Create(Action onDispose)
            => Create(onDispose != null ? _ => onDispose() : null);
    }
}