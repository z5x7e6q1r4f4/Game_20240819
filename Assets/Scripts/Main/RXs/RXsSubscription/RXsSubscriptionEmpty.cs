using System;

namespace Main.RXs 
{
    public class RXsSubscriptionEmpty : IRXsSubscription
    {
        public static readonly RXsSubscriptionEmpty Instance = new();
        void IDisposable.Dispose() { }
        void IRXsSubscription.Subscribe() { }
        void IRXsSubscription.Unsubscribe() { }
        private RXsSubscriptionEmpty() { }
    }
}