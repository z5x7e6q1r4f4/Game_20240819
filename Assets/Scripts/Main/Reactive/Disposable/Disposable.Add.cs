using System;

namespace Main
{
    partial class Disposable
    {
        public static IDisposable Add(this IDisposableContainer subscriptionHandler, Action<DisposableFromAction> action)
        {
            var sub = Create(action);
            subscriptionHandler.Add(sub);
            return sub;
        }
        public static IDisposable Add(this IDisposableContainer subscriptionHandler, Action action)
        {
            var sub = Create(action);
            subscriptionHandler.Add(sub);
            return sub;
        }
    }
}