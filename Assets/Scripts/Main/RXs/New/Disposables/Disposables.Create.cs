using System;

namespace Main.RXs
{
    public static partial class Disposables
    {
        public static IDisposableFromAction Create(Action<IDisposableFromAction> onDispose = null)
        {
            var disposable = DisposableFromAction.GetFromReusePool();
            disposable.OnDisposeAction += onDispose;
            return disposable;
        }
        public static IDisposableFromAction Create(Action onDispose)
            => Create(onDispose != null ? _ => onDispose() : null);
    }
}