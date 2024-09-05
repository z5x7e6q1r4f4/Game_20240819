using System;

namespace Main.RXs
{
    partial class RXsSubscription
    {
        public static IRXsDisposable AsSubscription(object obj)
            => obj as IRXsDisposable ?? Empty;
        public static readonly IRXsDisposable Empty = new RXsSubscriptionEmpty();
        private class RXsSubscriptionEmpty : IRXsDisposable
        {
            void IDisposable.Dispose() { }
        }
    }
}