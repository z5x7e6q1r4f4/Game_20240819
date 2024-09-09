using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    public interface ICollection : ICollectionReadonly
    {
        new object this[int index] { get; set; }
        IObservable<CollectionBeforeAdd> BeforeAdd { get; }
        IObservable<CollectionBeforeRemove> BeforeRemove { get; }
        int Add(object item, bool beforeAdd = true, bool afterAdd = true);
        void AddRange(IEnumerable collection, bool beforeAdd = true, bool afterAdd = true);
        void Clear(bool beforeRemove = true, bool afterRemove = true);
        int Insert(int index, object item, bool beforeAdd = true, bool afterAdd = true);
        int Remove(object item, bool beforeRemove = true, bool afterRemove = true);
        void RemvoeRange(IEnumerable collection, bool beforeRemove = true, bool afterRemove = true);
        int RemoveAt(int index, bool beforeRemove = true, bool afterRemove = true);
        void SetAt(int index, object value, bool indexCheck = true, bool invokeEvent = true);
    }
    public interface ICollection<T> : ICollection, ICollectionReadonly<T>
    {
        object ICollection.this[int index] { get => this[index]; set => this[index] = (T)value; }
        IObservable<CollectionBeforeAdd> ICollection.BeforeAdd => BeforeAdd;
        IObservable<CollectionBeforeRemove> ICollection.BeforeRemove => BeforeRemove;
        int ICollection.Add(object item, bool beforeAdd, bool afterAdd) => Add((T)item, beforeAdd, afterAdd);
        void ICollection.AddRange(IEnumerable collection, bool beforeAdd, bool afterAdd) => Add(collection.OfType<T>(), beforeAdd, afterAdd);
        int ICollection.Insert(int index, object item, bool beforeAdd, bool afterAdd) => Insert(index, (T)item, beforeAdd, afterAdd);
        int ICollection.Remove(object item, bool beforeRemove, bool afterRemove) => Remove((T)item, beforeRemove, afterRemove);
        void ICollection.RemvoeRange(IEnumerable collection, bool beforeRemove, bool afterRemove) => RemoveRange(collection.OfType<T>(), beforeRemove, afterRemove);
        void ICollection.SetAt(int index, object value, bool indexCheck, bool invokeEvent) => SetAt(index, (T)value, indexCheck, invokeEvent);
        //
        new T this[int index] { get; set; }
        new IObservable<CollectionBeforeAdd<T>> BeforeAdd { get; }
        new IObservable<CollectionBeforeRemove<T>> BeforeRemove { get; }
        int Add(T item, bool beforeAdd = true, bool afterAdd = true);
        void AddRange(IEnumerable<T> collection, bool beforeAdd = true, bool afterAdd = true);
        int Insert(int index, T item, bool beforeAdd = true, bool afterAdd = true);
        int Remove(T item, bool beforeRemove = true, bool afterRemove = true);
        void RemoveRange(IEnumerable<T> collection, bool beforeRemove = true, bool afterRemove = true);
        void SetAt(int index, T value, bool indexCheck = true, bool invokeEvent = true);
    }
}