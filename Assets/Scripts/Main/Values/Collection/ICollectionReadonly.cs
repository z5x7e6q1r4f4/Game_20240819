using System;
using System.Collections.Generic;
using System.Collections;

namespace Main
{
    public interface ICollectionReadonly : IEnumerable
    {
        int Count { get; }
        object this[int index] { get; }
        bool Contains(object item);
        int IndexOf(object item);
        object GetAt(int index, bool indexCheck = true);
        IObservableImmediately<CollectionAfterAdd> AfterAdd { get; }
        IObservable<CollectionAfterRemove> AfterRemove { get; }
    }
    public interface ICollectionReadonly<T> : ICollectionReadonly, IEnumerable<T>
    {
        object ICollectionReadonly.this[int index] => this[index];
        bool ICollectionReadonly.Contains(object item) => Contains((T)item);
        int ICollectionReadonly.IndexOf(object item) => IndexOf((T)item);
        object ICollectionReadonly.GetAt(int index, bool indexCheck) => GetAt(index, indexCheck);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IObservableImmediately<CollectionAfterAdd> ICollectionReadonly.AfterAdd => AfterAdd;
        IObservable<CollectionAfterRemove> ICollectionReadonly.AfterRemove => AfterRemove;
        //
        new T this[int index] { get; }
        bool Contains(T item);
        int IndexOf(T item);
        new T GetAt(int index, bool indexCheck = true);
        new IObservableImmediately<CollectionAfterAdd<T>> AfterAdd { get; }
        new IObservable<CollectionAfterRemove<T>> AfterRemove { get; }
    }
}