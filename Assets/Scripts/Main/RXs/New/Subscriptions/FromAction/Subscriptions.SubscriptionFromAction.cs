using System;

namespace Main.RXs
{
    public static partial class Subscriptions
    {
        private sealed class SubscriptionFromAction : Reuse.ObjectBase<SubscriptionFromAction>, ISubscriptionFromAction, IReuseable.IOnRelease
        {
            public event Action<ISubscriptionFromAction> OnDisposeAction;
            public void Dispose() => this.ReleaseToReusePool();
            public static SubscriptionFromAction GetFromReusePool() => GetFromReusePool(false);
            void IReuseable.IOnRelease.OnRelease()
            {
                OnDisposeAction?.Invoke(this);
                OnDisposeAction = null;
            }
        }

    }
}