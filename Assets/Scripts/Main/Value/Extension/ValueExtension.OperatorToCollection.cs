using System;
using System.Collections.Generic;

namespace Main
{
    partial class ValueExtension
    {
        private sealed class OperatorToCollection<T> :
            Reuse.ObjectBase<OperatorToCollection<T>>,
            IOperatorToCollection<T>,
            IReuseable.IOnRelease
        {
            private IDisposable disposable;
            private ICollection<T> collection;
            //Collection
            T ICollectionReadonly<T>.this[int index] => collection[index];
            IObservableImmediately<CollectionAfterAdd<T>> ICollectionReadonly<T>.AfterAdd => collection.AfterAdd;
            IObservable<CollectionAfterRemove<T>> ICollectionReadonly<T>.AfterRemove => collection.AfterRemove;
            int ICollectionReadonly.Count => collection.Count;
            bool ICollectionReadonly<T>.Contains(T item) => collection.Contains(item);
            T ICollectionReadonly<T>.GetAt(int index, bool indexCheck) => collection.GetAt(index, indexCheck);
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => collection.GetEnumerator();
            int ICollectionReadonly<T>.IndexOf(T item) => collection.IndexOf(item);
            //Reuse
            void IReuseable.IOnRelease.OnRelease()
            {
                disposable.Dispose();
                disposable = null;
                collection.Clear();
                collection = null;
            }
            void IDisposable.Dispose() => this.ReleaseToReusePool();
            public static OperatorToCollection<T> GetFromReusePool(IDisposable disposable, ICollection<T> collection)
            {
                var collectionSubscription = GetFromReusePool();
                collectionSubscription.disposable = disposable;
                collectionSubscription.collection = collection;
                return collectionSubscription;
            }
            private OperatorToCollection() { }
        }
    }
}