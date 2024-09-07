using System;
using System.Collections.Generic;
using System.Collections;

namespace Main.RXs
{
    public interface IObservableCollection_Readonly : IEnumerable
    {
        int Count { get; }
        object this[int index] { get; }
        bool Contains(object item);
        int IndexOf(object item);
        object GetAt(int index, bool indexCheck = true);
        IObservableImmediately<IObservableCollection_AfterAdd> AfterAdd { get; }
        IObservable<IObservableCollection_AfterRemove> AfterRemove { get; }
    }
    public interface IObservableCollection_Readonly<T> : IObservableCollection_Readonly, IEnumerable<T>
    {
        object IObservableCollection_Readonly.this[int index] => this[index];
        bool IObservableCollection_Readonly.Contains(object item) => Contains((T)item);
        int IObservableCollection_Readonly.IndexOf(object item) => IndexOf((T)item);
        object IObservableCollection_Readonly.GetAt(int index, bool indexCheck) => GetAt(index, indexCheck);
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        IObservableImmediately<IObservableCollection_AfterAdd> IObservableCollection_Readonly.AfterAdd => AfterAdd;
        IObservable<IObservableCollection_AfterRemove> IObservableCollection_Readonly.AfterRemove => AfterRemove;
        //
        new T this[int index] { get; }
        bool Contains(T item);
        int IndexOf(T item);
        new T GetAt(int index, bool indexCheck = true);
        new IObservableImmediately<IObservableCollection_AfterAdd<T>> AfterAdd { get; }
        new IObservable<IObservableCollection_AfterRemove<T>> AfterRemove { get; }
    }
}