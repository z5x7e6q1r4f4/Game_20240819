using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    public interface IRXsCollection : IRXsCollection_Readonly
    {
        new object this[int index] { get; set; }
        IObservable<IRXsCollection_BeforeAdd> BeforeAdd { get; }
        IObservable<IRXsCollection_BeforeRemove> BeforeRemove { get; }
        int Add(object item, bool beforeAdd = true, bool afterAdd = true);
        void AddRange(IEnumerable collection, bool beforeAdd = true, bool afterAdd = true);
        void Clear(bool beforeRemove = true, bool afterRemove = true);
        int Insert(int index, object item, bool beforeAdd = true, bool afterAdd = true);
        int Remove(object item, bool beforeRemove = true, bool afterRemove = true);
        int RemoveAt(int index, bool beforeRemove = true, bool afterRemove = true);
    }
    public interface IRXsCollection<T> : IRXsCollection, IRXsCollection_Readonly<T>
    {
        object IRXsCollection.this[int index] { get => this[index]; set => this[index] = (T)value; }
        IObservable<IRXsCollection_BeforeAdd> IRXsCollection.BeforeAdd => BeforeAdd;
        IObservable<IRXsCollection_BeforeRemove> IRXsCollection.BeforeRemove => BeforeRemove;
        int IRXsCollection.Add(object item, bool beforeAdd, bool afterAdd) => Add((T)item, beforeAdd, afterAdd);
        void IRXsCollection.AddRange(IEnumerable collection, bool beforeAdd, bool afterAdd) => Add(collection.OfType<T>(), beforeAdd, afterAdd);
        int IRXsCollection.Insert(int index, object item, bool beforeAdd, bool afterAdd) => Insert(index, (T)item, beforeAdd, afterAdd);
        int IRXsCollection.Remove(object item, bool beforeRemove, bool afterRemove) => Remove((T)item, beforeRemove, afterRemove);
        //
        new T this[int index] { get; set; }
        new IObservable<IRXsCollection_BeforeAdd<T>> BeforeAdd { get; }
        new IObservable<IRXsCollection_BeforeRemove<T>> BeforeRemove { get; }
        int Add(T item, bool beforeAdd = true, bool afterAdd = true);
        void AddRange(IEnumerable<T> collection, bool beforeAdd = true, bool afterAdd = true);
        int Insert(int index, T item, bool beforeAdd = true, bool afterAdd = true);
        int Remove(T item, bool beforeRemove = true, bool afterRemove = true);
    }
}