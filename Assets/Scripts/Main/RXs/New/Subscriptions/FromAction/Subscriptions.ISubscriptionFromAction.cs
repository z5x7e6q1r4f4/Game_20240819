using System;

namespace Main.RXs
{
    public static partial class Subscriptions
    {
        public interface ISubscriptionFromAction : ISubscription
        {
            event Action<ISubscriptionFromAction> OnDisposeAction;
        }

    }
}