using System;

namespace Main.RXs
{
    partial class Subscriptions
    {
        public static ISubscription Add(this ISubscriptionHandler subscriptionHandler, Action<ISubscriptionFromAction> action)
        {
            var sub = Create(action);
            subscriptionHandler.Add(sub);
            return sub;
        }
        public static ISubscription Add(this ISubscriptionHandler subscriptionHandler, Action action)
        {
            var sub = Create(action);
            subscriptionHandler.Add(sub);
            return sub;
        }
    }
}