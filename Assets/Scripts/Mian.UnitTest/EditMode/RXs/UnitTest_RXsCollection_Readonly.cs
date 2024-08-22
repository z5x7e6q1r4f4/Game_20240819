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
    public class UnitTest_RXsCollection_Readonly
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
        private IRXsCollection_Readonly<string> collection;
        [SetUp]
        public void SetUp()
        {
            Reuse.Clear();
            collection = new RXsCollection_SerializeField<string>(items);
            Assert.AreEqual(items.Count(), collection.Count);
            foreach (var item in items)
            {
                Assert.True(collection.Contains(item));
            }
        }
        [Test]
        public void AfterAdd_Data([Values(0, 1, 2)] int index, [Values("a", "b", "c")] string item)
        {
            collection.AfterAdd.Subscribe(e =>
            {
                Assert.AreEqual(collection, e.Collection);
                Assert.AreEqual(index, e.Index);
                Assert.AreEqual(item, e.Item);
            });
            (collection as RXsCollection_SerializeField<string>).Insert(index, item);
        }
        [Test]
        public void AfterAdd_Count([Values] bool afterAdd)
        {
            var count = 0;
            collection.AfterAdd.Subscribe(e => count++);
            (collection as RXsCollection_SerializeField<string>).Add(newItem, afterAdd: afterAdd);
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
        public void AfterRemove_Data([ValueSource(nameof(items))] string item)
        {
            var index = collection.IndexOf(item);
            collection.AfterRemove.Subscribe(e =>
            {
                Assert.AreEqual(collection, e.Collection);
                Assert.AreEqual(index, e.Index);
                Assert.AreEqual(item, e.Item);
            });
            (collection as RXsCollection_SerializeField<string>).Remove(item);
        }
        [Test]
        public void AfterRemove_Count([Values] bool afterRemove)
        {
            var count = 0;
            collection.AfterRemove.Subscribe(e => count++);
            (collection as RXsCollection_SerializeField<string>).RemoveAt(0, afterRemove: afterRemove);
            if (afterRemove) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
    }
}