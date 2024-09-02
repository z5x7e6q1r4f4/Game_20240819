using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public class RXsSubscriptionDictonary : IRXsSubscription
    {
        private Dictionary<object, IRXsSubscription> subscriptions = new();
        public void Add(object key, IRXsSubscription disposable) => subscriptions.Add(key, disposable);
        public IRXsSubscription Remove(object key)
        {
            if (subscriptions.TryGetValue(key, out var disposable))
            {
                subscriptions.Remove(key);
                return disposable;
            }
            else return RXsSubscriptionEmpty.Instance;
        }
        public bool ContainsKey(object key) => subscriptions.ContainsKey(key);
        public void Dispose(object key) => Remove(key).Dispose();
        public void Dispose() { Unsubscribe(); subscriptions.Clear(); }
        public void Subscribe()
        { foreach (var subscription in subscriptions.Values) subscription.Subscribe(); }
        public void Unsubscribe()
        { foreach (var subscription in subscriptions.Values) subscription.Unsubscribe(); }
    }
}