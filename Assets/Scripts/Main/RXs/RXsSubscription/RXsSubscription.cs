using System;
using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    public static class RXsSubscription
    {
        private class RXsSubscriptionFromAction :
            Reuse.ObjectBase<RXsSubscriptionFromAction>,
            IReuseable.IOnRelease,
            IRXsSubscription
        {
            Action onSubscribe;
            Action onUnsubscribe;
            Action onRelease;
            public void Dispose() => this.ReleaseToReusePool();
            public void Subscribe() => onSubscribe?.Invoke();
            public void Unsubscribe() => onUnsubscribe?.Invoke();
            public void OnRelease()
            {
                Unsubscribe();
                onRelease?.Invoke();
                onSubscribe = null;
                onUnsubscribe = null;
                onRelease = null;
            }
            public static RXsSubscriptionFromAction GetFromReusePool(Action onSubscribe, Action onUnsubscribe, Action onRelease)
            {
                var subscription = GetFromReusePool();
                subscription.onSubscribe = onSubscribe;
                subscription.onUnsubscribe = onUnsubscribe;
                subscription.onRelease = onRelease;
                return subscription;
            }
            private RXsSubscriptionFromAction() { }
        }
        public static IRXsSubscription FromAction(Action onSubscribe = null, Action onUnsubscribe = null, Action onRelease = null)
            => RXsSubscriptionFromAction.GetFromReusePool(onSubscribe, onUnsubscribe, onRelease);
        public static IRXsSubscription FromList(IEnumerable<IRXsSubscription> subscriptions)
        {
            List<IRXsSubscription> list = new(subscriptions);
            return FromAction(
                    () => { foreach (var item in list) item.Subscribe(); },
                    () => { foreach (var item in list) item.Unsubscribe(); },
                    () => { foreach (var item in list) { item.Dispose(); } list.Clear(); list = null; }
                );
        }
        public static IRXsSubscription FromList(params IRXsSubscription[] subscriptions)
            => FromList(subscriptions.AsEnumerable());
    }
}