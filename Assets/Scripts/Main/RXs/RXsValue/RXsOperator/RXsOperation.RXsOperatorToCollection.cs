using System;
using System.Collections.Generic;

namespace Main.RXs
{
    partial class Operation
    {
        private sealed class RXsOperatorToCollection<T> :
            Reuse.ObjectBase<RXsOperatorToCollection<T>>,
            IRXsOperatorToCollection<T>,
            IReuseable.IOnRelease
        {
            private IRXsDisposable disposables;
            private IRXsCollection<T> collection;
            private Action onRelease;
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
                disposables.Dispose();
                disposables = null;
                collection.Clear();
                collection = null;
                onRelease?.Invoke();
                onRelease = null;
            }
            void IDisposable.Dispose() => this.ReleaseToReusePool();
            public static RXsOperatorToCollection<T> GetFromReusePool(IRXsDisposable subscription, IRXsCollection<T> collection)
            {
                var collectionSubscription = GetFromReusePool();
                collectionSubscription.disposables = subscription;
                collectionSubscription.collection = collection;
                return collectionSubscription;
            }
            private RXsOperatorToCollection() { }
        }
    }
}