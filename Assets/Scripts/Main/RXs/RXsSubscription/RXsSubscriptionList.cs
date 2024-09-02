using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public class RXsSubscriptionList : IRXsSubscription
    {
        private readonly List<IRXsSubscription> subscriptions = new();
        public int Count => subscriptions.Count;
        public void Add(IRXsSubscription disposable) => subscriptions.Add(disposable);
        public void Remove(IRXsSubscription disposable) => subscriptions.Remove(disposable);
        public void Dispose()
        {
            foreach (var subscription in subscriptions) subscription.Dispose();
            subscriptions.Clear();
        }
        public void Subscribe()
        { foreach (var subscription in subscriptions) subscription.Subscribe(); }
        public void Unsubscribe()
        { foreach (var subscription in subscriptions) subscription.Unsubscribe(); }
        public RXsSubscriptionList(params IRXsSubscription[] disposables) => subscriptions.AddRange(disposables);
    }
}