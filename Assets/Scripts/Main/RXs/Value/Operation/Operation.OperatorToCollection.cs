using System;
using System.Collections.Generic;

namespace Main.RXs
{
    partial class Operation
    {
        private sealed class OperatorToCollection<T> :
            Reuse.ObjectBase<OperatorToCollection<T>>,
            IOperatorToCollection<T>,
            IReuseable.IOnRelease
        {
            private IDisposable disposable;
            private IObservableCollection<T> collection;
            //Collection
            T IObservableCollection_Readonly<T>.this[int index] => collection[index];
            IObservableImmediately<IObservableCollection_AfterAdd<T>> IObservableCollection_Readonly<T>.AfterAdd => collection.AfterAdd;
            IObservable<IObservableCollection_AfterRemove<T>> IObservableCollection_Readonly<T>.AfterRemove => collection.AfterRemove;
            int IObservableCollection_Readonly.Count => collection.Count;
            bool IObservableCollection_Readonly<T>.Contains(T item) => collection.Contains(item);
            T IObservableCollection_Readonly<T>.GetAt(int index, bool indexCheck) => collection.GetAt(index, indexCheck);
            IEnumerator<T> IEnumerable<T>.GetEnumerator() => collection.GetEnumerator();
            int IObservableCollection_Readonly<T>.IndexOf(T item) => collection.IndexOf(item);
            //Reuse
            void IReuseable.IOnRelease.OnRelease()
            {
                disposable.Dispose();
                disposable = null;
                collection.Clear();
                collection = null;
            }
            void IDisposable.Dispose() => this.ReleaseToReusePool();
            public static OperatorToCollection<T> GetFromReusePool(IDisposable disposable, IObservableCollection<T> collection)
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