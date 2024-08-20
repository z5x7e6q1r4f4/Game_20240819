using System;
using System.Collections;
using System.Collections.Generic;

namespace Main.RXs
{
    public abstract partial class RXsCollection<T> : IRXsCollection<T>
    {
        protected abstract List<T> SerializedCollection { get; }
        public int Count => SerializedCollection.Count;
        public T this[int index]
        {
            get => GetAt(index, false);
            set => SetAt(index, value, false);
        }
        //Core
        private void SetAt(int index, T value, bool indexCheck = true, bool invokeEvent = true)
        {
            if (indexCheck && (index < 0 || index >= Count)) return;
            RemoveAt(index, invokeEvent, invokeEvent);
            Insert(index, value, invokeEvent, invokeEvent);
        }
        private T GetAt(int index, bool indexCheck = true)
        {
            if (indexCheck && (index < 0 || index >= Count)) return default;
            return this[index];
        }
        protected int AddCore(int index, T item, bool beforeAdd, bool afterAdd)
        {
            if (index < 0 || index > Count) throw new ArgumentOutOfRangeException();
            if (beforeAdd && !this.beforeAdd.Invoke(this, index, item, out item)) return -1;
            SerializedCollection.Insert(index, item);
            if (afterAdd) this.afterAdd.Invoke(this, index, item, out _);
            return index;
        }
        protected int RemoveCore(int index, T item, bool beforeRemove, bool afterRemove)
        {
            if (index < 0 || index >= Count) throw new ArgumentOutOfRangeException();
            if (beforeRemove && !this.beforeRemove.Invoke(this, index, item, out _)) return -1;
            SerializedCollection.RemoveAt(index);
            if (afterRemove) this.afterRemove.Invoke(this, index, item, out _);
            return index;
        }
        //Add
        public int Add(T item, bool beforeAdd = true, bool afterAdd = true) => AddCore(Count, item, beforeAdd, afterAdd);
        public void AddRange(IEnumerable<T> collection, bool beforeAdd = true, bool afterAdd = true) { foreach (var item in collection) Add(item, beforeAdd, afterAdd); }
        public int Insert(int index, T item, bool beforeAdd = true, bool afterAdd = true) => AddCore(index, item, beforeAdd, afterAdd);
        //Remove
        public int Remove(T item, bool beforeRemove = true, bool afterRemove = true) => RemoveCore(IndexOf(item), item, beforeRemove, afterRemove);
        public int RemoveAt(int index, bool beforeRemove = true, bool afterRemove = true) => RemoveCore(index, GetAt(index), beforeRemove, afterRemove);
        public void Clear(bool beforeRemove = true, bool afterRemove = true)
        {
            var index = 0;
            while (index < Count)
            {
                if (RemoveAt(index, beforeRemove, afterRemove) >= 0) index++;
            }
        }
        //Search
        public int IndexOf(T item) => SerializedCollection.IndexOf(item);
        public bool Contains(T item) => SerializedCollection.Contains(item);
        //Event
        public IObservable<IRXsCollection_BeforeAdd<T>> BeforeAdd => beforeAdd;
        public IObservableImmediately<IRXsCollection_AfterAdd<T>> AfterAdd => afterAdd;
        public IObservable<IRXsCollection_BeforeRemove<T>> BeforeRemove => beforeRemove;
        public IObservable<IRXsCollection_AfterRemove<T>> AfterRemove => afterRemove;
        private readonly EventHandler beforeAdd;
        private readonly EventHandler afterAdd;
        private readonly EventHandler beforeRemove;
        private readonly EventHandler afterRemove;
        //IEnumerable
        IEnumerator<T> IEnumerable<T>.GetEnumerator() { foreach (var item in SerializedCollection) yield return item; }
        public RXsCollection(IEnumerable<T> collection = null)
        {
            beforeAdd = new();
            afterAdd = new(new AfterAddImmediately(this));
            beforeRemove = new();
            afterRemove = new();
            if (collection != null) AddRange(collection);
        }
    }
}