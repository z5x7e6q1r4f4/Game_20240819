using System;
using System.Collections.Generic;
using System.Collections;

namespace Main.RXs
{
    public interface IRXsCollection_Readonly : IEnumerable
    {
        int Count { get; }
        object this[int index] { get; }
        bool Contains(object item);
        int IndexOf(object item);
        object GetAt(int index, bool indexCheck = true);
        IObservableImmediately<IRXsCollection_AfterAdd> AfterAdd { get; }
        IRXsObservable<IRXsCollection_AfterRemove> AfterRemove { get; }
    }
    public interface IRXsCollection_Readonly<T> : IRXsCollection_Readonly, IEnumerable<T>
    {
        object IRXsCollection_Readonly.this[int index] => this[index];
        bool IRXsCollection_Readonly.Contains(object item) => Contains((T)item);
        int IRXsCollection_Readonly.IndexOf(object item) => IndexOf((T)item);
        object IRXsCollection_Readonly.GetAt(int index, bool indexCheck) => GetAt(index, indexCheck);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IObservableImmediately<IRXsCollection_AfterAdd> IRXsCollection_Readonly.AfterAdd => AfterAdd;
        IRXsObservable<IRXsCollection_AfterRemove> IRXsCollection_Readonly.AfterRemove => AfterRemove;
        //
        new T this[int index] { get; }
        bool Contains(T item);
        int IndexOf(T item);
        new T GetAt(int index, bool indexCheck = true);
        new IObservableImmediately<IRXsCollection_AfterAdd<T>> AfterAdd { get; }
        new IRXsObservable<IRXsCollection_AfterRemove<T>> AfterRemove { get; }
    }
}