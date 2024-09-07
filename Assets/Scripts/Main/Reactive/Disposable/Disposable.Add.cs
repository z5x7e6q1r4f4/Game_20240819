using System;

namespace Main
{
    partial class Disposable
    {
        public static DisposableFromAction Add(this IDisposableHandler subscriptionHandler, Action<DisposableFromAction> action)
        {
            var sub = Create(action);
            subscriptionHandler.Add(sub);
            return sub;
        }
        public static DisposableFromAction Add(this IDisposableHandler subscriptionHandler, Action action)
        {
            var sub = Create(action);
            subscriptionHandler.Add(sub);
            return sub;
        }
    }
}