using System;

namespace Main.RXs
{
    public static partial class Subscriptions
    {
        public static ISubscriptionFromAction Create(Action<ISubscriptionFromAction> onDispose = null)
        {
            var subscription = SubscriptionFromAction.GetFromReusePool();
            subscription.OnDisposeAction += onDispose;
            return subscription;
        }
        public static ISubscriptionFromAction Create(Action onDispose)
            => Create(_ => onDispose());
    }
}