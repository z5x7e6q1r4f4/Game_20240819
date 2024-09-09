using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    public static partial class Disposable
    {
        public static DisposableFromAction Create(Action<IDisposableBase> onDispose = null)
            => DisposableFromAction.GetFromReusePool(onDispose);
        public static DisposableFromAction Create(Action onDispose)
            => Create(_ => onDispose());
        public static DisposableFromAction Create(IEnumerable<IDisposable> disposables)
            => Create(() => { foreach (var disposable in disposables) disposable.Dispose(); });
        public static DisposableFromAction Create(params IDisposable[] disposables)
            => Create(disposables.AsEnumerable());
    }
}