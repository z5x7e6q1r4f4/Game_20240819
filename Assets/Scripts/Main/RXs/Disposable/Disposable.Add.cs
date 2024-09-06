using System;

namespace Main.RXs
{
    partial class Disposable
    {
        public static IDisposable Add(this IDisposableContainer subscriptionHandler, Action<IDisposableFromAction> action)
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