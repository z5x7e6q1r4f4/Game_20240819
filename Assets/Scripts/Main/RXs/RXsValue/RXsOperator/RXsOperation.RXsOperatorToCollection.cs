using System;
using System.Collections.Generic;

namespace Main.RXs
{
    partial class RXsOperation
    {
        private sealed class RXsOperatorToCollection<T> :
            Reuse.ObjectBase<RXsOperatorToCollection<T>>,
            IRXsOperatorToCollection<T>,
            IReuseable.IOnRelease
        {
            private IRXsSubscription subscription;
            private IRXsCollection<T> collection;
            private Action onRelease;
            //Subscription
            void IRXsSubscription.Subscribe() => subscription.Subscribe();
            void IRXsSubscription.Unsubscribe() { subscription.Unsubscribe(); collection.Clear(); }
            //Collection
            T IRXsCollection_Readonly<T>.this[int index] => collection[index];
            IRXsObservableImmediately<IRXsCollection_AfterAdd<T>> IRXsCollection_Readonly<T>.AfterAdd => collection.AfterAdd;
            IRXsObservable<IRXsCollection_AfterRemove<T>> IRXsCollection_Readonly<T>.AfterRemove => collection.AfterRemove;
            int IRXsCollection_Readonly.Count => collection.Count;
            bool IRXsCollection_Readonly<T>.Contains(T item) => collection.Contains(item);
            T IRXsCollection_Readonly<T>.GetAt(int index, bool indexCheck) => collection.GetAt(index, indexCheck);
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => collection.GetEnumerator();
            int IRXsCollection_Readonly<T>.IndexOf(T item) => collection.IndexOf(item);
            //Reuse
            void IReuseable.IOnRelease.OnRelease()
            {
                subscription.Dispose();
                subscription = null;
                collection.Clear();
                collection = null;
                onRelease?.Invoke();
                onRelease = null;
            }
            void IDisposable.Dispose() => this.ReleaseToReusePool();
            public static RXsOperatorToCollection<T> GetFromReusePool(IRXsSubscription subscription, IRXsCollection<T> collection)
            {
                var collectionSubscription = GetFromReusePool();
                collectionSubscription.subscription = subscription;
                collectionSubscription.collection = collection;
                return collectionSubscription;
            }
            private RXsOperatorToCollection() { }
        }
    }
}