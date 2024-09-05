namespace Main.RXs
{
    partial class Subscriptions
    {
        public static void RemoveAndDispose(this ISubscriptionHandler subscriptionHandler, ISubscription subscription)
        {
            if (subscriptionHandler.Remove(subscription) &&
                subscriptionHandler.Count == 0)
                subscriptionHandler.Dispose();
        }
    }
}