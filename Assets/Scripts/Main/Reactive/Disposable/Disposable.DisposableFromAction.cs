using System;

namespace Main
{
    partial class Disposable
    {
        public sealed class DisposableFromAction :
            DisposableBaseReuseable<DisposableFromAction>,
            IReuseable.IOnRelease
        {
            public static DisposableFromAction GetFromReusePool(Action<IDisposableBase> onDispose)
            {
                var disposable = GetFromReusePool(false);
                disposable.OnDisposeAction += onDispose;
                return disposable;
            }
        }
    }
}