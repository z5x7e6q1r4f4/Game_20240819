using System;
using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    public static partial class RXsSubscription
    {
        private class RXsSubscriptionFromAction :
            Reuse.ObjectBase<RXsSubscriptionFromAction>,
            IReuseable.IOnRelease,
            IRXsDisposable
        {
            Action onDispose;
            public void Dispose() => this.ReleaseToReusePool();
            public void OnRelease()
            {
                onDispose?.Invoke();
                onDispose = null;
            }
            public static RXsSubscriptionFromAction GetFromReusePool(Action onRelease)
            {
                var subscription = GetFromReusePool();
                subscription.onDispose = onRelease;
                return subscription;
            }
            private RXsSubscriptionFromAction() { }
        }
        public static IRXsDisposable FromAction(Action onDispose = null)
            => RXsSubscriptionFromAction.GetFromReusePool(onDispose);
        public static IRXsDisposable FromList(IEnumerable<IRXsDisposable> subscriptions)
        {
            List<IRXsDisposable> list = new(subscriptions);
            return FromAction(
                    () => { foreach (var item in list) { item.Dispose(); } list.Clear(); list = null; }
                );
        }
        public static IRXsDisposable FromList(params IRXsDisposable[] subscriptions)
            => FromList(subscriptions.AsEnumerable());
        public static IRXsDisposable Add(this IRXsDisposable subscription, params IRXsDisposable[] subscriptions)
            => FromList(subscription, FromList(subscriptions));
        public static IRXsDisposable Add(this IRXsDisposable subscription,  Action onRelease = null)
            => FromList(subscription, FromAction(onRelease));
        public static void Until<T>(this IRXsDisposable subscription, IRXsObservable<T> onNext)
            => onNext.Take(1).Subscribe((self, _) => { subscription.Dispose(); self.Dispose(); });
        public static void Until<T>(this IRXsDisposable subscription, IRXsObservable onNext)
            => onNext.Take(1).Subscribe((self, _) => { subscription.Dispose(); self.Dispose(); });
    }
}