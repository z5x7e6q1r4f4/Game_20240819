using System;
using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    public static partial class Disposable
    {
        public static IDisposableFromAction Create(Action<IDisposableFromAction> onDispose = null)
        {
            var disposable = DisposableFromAction.GetFromReusePool();
            disposable.OnDisposeAction += onDispose;
            return disposable;
        }
        public static IDisposableFromAction Create(IEnumerable<IDisposable> disposables)
            => Create(_ => { foreach (var disposable in disposables) disposable.Dispose(); });
        public static IDisposableFromAction Create(params IDisposable[] disposables)
            => Create(disposables.AsEnumerable());
        public static IDisposableFromAction Create(Action onDispose)
            => Create(onDispose != null ? _ => onDispose() : null);
    }
}