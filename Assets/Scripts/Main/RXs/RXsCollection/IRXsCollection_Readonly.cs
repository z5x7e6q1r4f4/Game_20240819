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
        IObservableImmediately<IRXsCollection_AfterAdd> AfterAdd { get; }
        IObservable<IRXsCollection_AfterRemove> AfterRemove { get; }
    }
    public interface IRXsCollection_Readonly<T> : IRXsCollection_Readonly, IEnumerable<T>
    {
        bool IRXsCollection_Readonly.Contains(object item) => Contains((T)item);
        int IRXsCollection_Readonly.IndexOf(object item) => IndexOf((T)item);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IObservableImmediately<IRXsCollection_AfterAdd> IRXsCollection_Readonly.AfterAdd => AfterAdd;
        IObservable<IRXsCollection_AfterRemove> IRXsCollection_Readonly.AfterRemove => AfterRemove;
        //
        new T this[int index] { get; }
        bool Contains(T item);
        int IndexOf(T item);
        new IObservableImmediately<IRXsCollection_AfterAdd<T>> AfterAdd { get; }
        new IObservable<IRXsCollection_AfterRemove<T>> AfterRemove { get; }
    }
}