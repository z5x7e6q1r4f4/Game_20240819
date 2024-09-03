using System;

namespace Main.RXs
{
    partial class RXsSubscription
    {
        public static IRXsSubscription AsSubscription(object obj)
            => obj as IRXsSubscription ?? Empty;
        public static readonly IRXsSubscription Empty = new RXsSubscriptionEmpty();
        private class RXsSubscriptionEmpty : IRXsSubscription
        {
            void IDisposable.Dispose() { }
            void IRXsSubscription.Subscribe() { }
            void IRXsSubscription.Unsubscribe() { }
        }
    }
}