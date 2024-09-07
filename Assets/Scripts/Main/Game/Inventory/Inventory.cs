using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.Game
{
    public class Inventory : BodyPartComponent, IInventoryTest, ICollection<Item>
    {
        protected ICollection<Item> Items => AwakeSelf<Inventory>().items;
        [SerializeField] private CollectionSerializeField<Item> items = new();
        public IProperty<int> Capacity => AwakeSelf<Inventory>().capacity;
        [SerializeField] private PropertySerializeField<int> capacity = new();
        public int Remain => Capacity.Value - Items.Count;
        protected override void OnGameComponentAwake()
        {
            Items.BeforeAdd.Subscribe(e =>
            {
                if (e.Index >= Capacity.Value) e.IsEnable = false;
            });
            testItems.BeforeAdd.Subscribe(e =>
            {
                if (e.Index >= Capacity.Value) e.IsEnable = false;
            });
        }
        public Item this[int index] { get => Items[index]; set => Items[index] = value; }
        public int Count => Items.Count;
        public int Add(Item item, bool beforeAdd = true, bool afterAdd = true) => Items.Add(item, beforeAdd, afterAdd);
        public void AddRange(IEnumerable<Item> collection, bool beforeAdd = true, bool afterAdd = true) => Items.AddRange(collection, beforeAdd, afterAdd);
        public int Insert(int index, Item item, bool beforeAdd = true, bool afterAdd = true) => Items.Insert(index, item, beforeAdd, afterAdd);
        public int Remove(Item item, bool beforeRemove = true, bool afterRemove = true) => Items.Remove(item, beforeRemove, afterRemove);
        public void RemoveRange(IEnumerable<Item> collection, bool beforeRemove = true, bool afterRemove = true) => Items.RemoveRange(collection, beforeRemove, afterRemove);
        public int RemoveAt(int index, bool beforeRemove = true, bool afterRemove = true) => Items.RemoveAt(index, beforeRemove, afterRemove);
        public void Clear(bool beforeRemove = true, bool afterRemove = true) => Items.Clear(beforeRemove, afterRemove);
        public IObservableImmediately<CollectionAfterAdd<Item>> AfterAdd => Items.AfterAdd;
        public IObservable<CollectionAfterRemove<Item>> AfterRemove => Items.AfterRemove;
        public IObservable<CollectionBeforeAdd<Item>> BeforeAdd => Items.BeforeAdd;
        public IObservable<CollectionBeforeRemove<Item>> BeforeRemove => Items.BeforeRemove;
        public bool Contains(Item item) => Items.Contains(item);
        public int IndexOf(Item item) => Items.IndexOf(item);
        public Item GetAt(int index, bool indexCheck = true) => Items.GetAt(index, indexCheck);
        public void SetAt(int index, Item value, bool indexCheck = true, bool invokeEvent = true) => Items.SetAt(index, value, indexCheck, invokeEvent);
        public IEnumerator<Item> GetEnumerator() => Items.GetEnumerator();
        //Test
        protected CollectionSerializeField<Item> testItems { get; } = new();
        private bool testResult = true;
        public IInventoryTest Test()
        {
            testResult = true;
            testItems.Clear();
            testItems.AddRange(Items);
            return this;
        }
        IInventoryTest IInventoryTest.Add(Item item)
        {
            if (testResult) testResult = testItems.Add(item) >= 0;
            return this;
        }
        IInventoryTest IInventoryTest.AddRange(IEnumerable<Item> items)
        {
            foreach (var item in items) (this as IInventoryTest).Add(item);
            return this;
        }
        IInventoryTest IInventoryTest.Remove(Item item)
        {
            if (testResult) testResult = testItems.Remove(item) >= 0;
            return this;
        }
        IInventoryTest IInventoryTest.RemoveRange(IEnumerable<Item> items)
        {
            foreach (var item in items) (this as IInventoryTest).Remove(item);
            return this;
        }
        bool IInventoryTest.Result() => testResult;
    }
}
