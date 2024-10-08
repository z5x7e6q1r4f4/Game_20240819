using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using NSubstitute;
using UnityEngine;
using UnityEngine.TestTools;
using System;
using System.Reflection;

namespace Main.RXs
{
    public class UnitTest_RXsCollection
    {
        private static IEnumerable<string> items
        {
            get
            {
                yield return "Item1";
                yield return "Item2";
                yield return "Item3";
            }
        }
        private static IEnumerable<string> newItems
        {
            get
            {
                yield return "NewItem1";
                yield return "NewItem2";
                yield return "NewItem3";
            }
        }
        private static string item => items.First();
        private static string newItem => newItems.First();
        private RXsCollection_SerializeField<string> collection;
        [SetUp]
        public void SetUp()
        {
            Reuse.Clear();
            collection = new(items);
            Assert.AreEqual(items.Count(), collection.Count);
            foreach (var item in items)
            {
                Assert.True(collection.Contains(item));
            }
        }
        [Test]
        public void Add([Values] bool beforeAdd, [Values] bool afterAdd)
        {
            collection.Add(newItem, beforeAdd, afterAdd);
            Assert.AreEqual(items.Count() + 1, collection.Count);
            Assert.IsTrue(collection.Contains(newItem));
        }
        [Test]
        public void AddRange([Values] bool beforeAdd, [Values] bool afterAdd)
        {
            collection.AddRange(newItems, beforeAdd, afterAdd);
            Assert.AreEqual(items.Count() + newItems.Count(), collection.Count);
            foreach (var item in newItems) { Assert.IsTrue(collection.Contains(item)); }
        }
        [Test]
        public void Insert([Values(-1, 0, 1, 2, 3, 4, 5)] int index, [Values] bool beforeAdd, [Values] bool afterAdd)
        {
            var isError = index > collection.Count() || index < 0;
            if (isError) { Assert.Catch(() => collection.Insert(index, newItem, beforeAdd, afterAdd)); return; }
            collection.Insert(index, newItem, beforeAdd, afterAdd);
            Assert.AreEqual(index, collection.IndexOf(newItem));
            Assert.AreEqual(newItem, collection[index]);
        }
        [Test]
        public void Remove([Values] bool beforeRemove, [Values] bool afterRemove)
        {
            collection.Remove(item, beforeRemove, afterRemove);
            Assert.AreEqual(items.Count() - 1, collection.Count);
            Assert.IsFalse(collection.Contains(item));
        }
        [Test]
        public void RemoveAt([Values(-1, 0, 1, 2, 3)] int index, [Values] bool beforeRemove, [Values] bool afterRemove)
        {
            var isError = index >= collection.Count() || index < 0;
            if (isError) { Assert.Catch(() => collection.RemoveAt(index, beforeRemove, afterRemove)); return; }
            collection.RemoveAt(index, beforeRemove, afterRemove);
            Assert.AreEqual(items.Count() - 1, collection.Count);
        }
        //Event
        [Test]
        public void BaforeAdd_Data([Values(0, 1, 2)] int index, [Values("a", "b", "c")] string item)
        {
            collection.BeforeAdd.Subscribe(e =>
            {
                Assert.AreEqual(collection, e.Collection);
                Assert.AreEqual(index, e.Index);
                Assert.AreEqual(item, e.Item);
                Assert.AreEqual(item, e.Modified);
            });
            collection.Insert(index, item);
        }
        [Test]
        public void BaforeAdd_Count([Values] bool beforeAdd)
        {
            var count = 0;
            collection.BeforeAdd.Subscribe(e => count++);
            collection.Add(newItem, beforeAdd: beforeAdd);
            Assert.AreEqual(beforeAdd ? 1 : 0, count);
        }
        [Test]
        public void BaforeAdd_IsEnable([Values] bool isEnable)
        {
            collection.BeforeAdd.Subscribe(e => e.IsEnable = isEnable);
            collection.Add(newItem);
            Assert.AreEqual(isEnable ? items.Count() + 1 : items.Count(), collection.Count);
        }
        [Test]
        public void BaforeAdd_Modified([Values(null, "modified")] string modified)
        {
            var useModified = modified != null;
            collection.BeforeAdd.Subscribe(e => { if (useModified) e.Modified = modified; });
            collection.Add(newItem);
            Assert.AreEqual(useModified ? modified : newItem, collection[collection.Count - 1]);
        }
        //
        [Test]
        public void AfterAdd_Data([Values(0, 1, 2)] int index, [Values("a", "b", "c")] string item)
        {
            collection.AfterAdd.Subscribe(e =>
            {
                Assert.AreEqual(collection, e.Collection);
                Assert.AreEqual(index, e.Index);
                Assert.AreEqual(item, e.Item);
            });
            collection.Insert(index, item);
        }
        [Test]
        public void AfterAdd_Count([Values] bool afterAdd)
        {
            var count = 0;
            collection.AfterAdd.Subscribe(e => count++);
            collection.Add(newItem, afterAdd: afterAdd);
            if (afterAdd) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void AfterAdd_Immediately()
        {
            var count = 0;
            collection.AfterAdd.Immediately().Subscribe(e => count++);
            Assert.AreEqual(count, collection.Count);
        }
        [Test]
        public void AfterAdd_IsEnable([Values] bool isEnable)
        {
            var count = 0;
            collection.BeforeAdd.Subscribe(e => e.IsEnable = isEnable);
            collection.AfterAdd.Subscribe(e => count++);
            collection.Add(newItem);
            if (isEnable) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void AfterAdd_Modified([Values(null, "modified")] string modified)
        {
            var useModified = modified != null;
            int count = 0;
            collection.BeforeAdd.Subscribe(e => { if (useModified) e.Modified = modified; });
            collection.AfterAdd.Subscribe(e =>
            {
                count++;
                if (useModified) Assert.AreEqual(modified, e.Item);
                else Assert.AreEqual(newItem, e.Item);
            });
            collection.Add(newItem);
            Assert.AreEqual(1, count);
        }
        //
        [Test]
        public void BeforeRemove_Data([ValueSource(nameof(items))] string item)
        {
            var index = collection.IndexOf(item);
            collection.BeforeRemove.Subscribe(e =>
            {
                Assert.AreEqual(collection, e.Collection);
                Assert.AreEqual(index, e.Index);
                Assert.AreEqual(item, e.Item);
                Assert.IsTrue(e.IsEnable);
            });
            collection.Remove(item);
        }
        [Test]
        public void BeforeRemove_Count([Values] bool beforeRemove)
        {
            var count = 0;
            collection.BeforeRemove.Subscribe(e => count++);
            collection.RemoveAt(0, beforeRemove: beforeRemove);
            if (beforeRemove) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void BeforeRemove_IsEnable([Values] bool isEnable)
        {
            collection.BeforeRemove.Subscribe(e => e.IsEnable = isEnable);
            collection.RemoveAt(1);
            if (isEnable) Assert.AreEqual(items.Count() - 1, collection.Count);
            else Assert.AreEqual(items.Count(), collection.Count);
        }
        //
        [Test]
        public void AfterRemove_Data([ValueSource(nameof(items))] string item)
        {
            var index = collection.IndexOf(item);
            collection.AfterRemove.Subscribe(e =>
            {
                Assert.AreEqual(collection, e.Collection);
                Assert.AreEqual(index, e.Index);
                Assert.AreEqual(item, e.Item);
            });
            collection.Remove(item);
        }
        [Test]
        public void AfterRemove_Count([Values] bool afterRemove)
        {
            var count = 0;
            collection.AfterRemove.Subscribe(e => count++);
            collection.RemoveAt(0, afterRemove: afterRemove);
            if (afterRemove) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void AfterRemove_IsEnable([Values] bool isEnable)
        {
            var count = 0;
            collection.BeforeRemove.Subscribe(e => e.IsEnable = isEnable);
            collection.AfterRemove.Subscribe(e => count++);
            collection.RemoveAt(1);
            if (isEnable) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
    }
}