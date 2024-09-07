using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Main.RXs.ObservableCollections
{
    public class UnitTest_RXsCollection_Readonly : UnitTest_RXsCollection_Base
    {
        private new ICollectionReadonly<string> collection => base.collection;
        [Test]
        public void Test_AfterAdd_Data([Values(0, 1, 2)] int index, [Values("a", "b", "c")] string item)
        {
            using var _ = collection.AfterAdd.Subscribe(e =>
                {
                    Assert.AreEqual(collection, e.Collection);
                    Assert.AreEqual(index, e.Index);
                    Assert.AreEqual(item, e.Item);
                });
            (collection as CollectionSerializeField<string>).Insert(index, item);
        }
        [Test]
        public void Test_AfterAdd_Count([Values] bool afterAdd)
        {
            var count = 0;
            using var _ = collection.AfterAdd.Subscribe(e => count++);
            (collection as CollectionSerializeField<string>).Add(newItem, afterAdd: afterAdd);
            if (afterAdd) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void Test_AfterAdd_Immediately()
        {
            var count = 0;
            using var _ = collection.AfterAdd.Immediately().Subscribe(e => count++);
            Assert.AreEqual(count, collection.Count);
        }
        [Test]
        public void Test_AfterRemove_Data([ValueSource(nameof(items))] string item)
        {
            var index = collection.IndexOf(item);
            using var _ = collection.AfterRemove.Subscribe(e =>
            {
                Assert.AreEqual(collection, e.Collection);
                Assert.AreEqual(index, e.Index);
                Assert.AreEqual(item, e.Item);
            });
            (collection as CollectionSerializeField<string>).Remove(item);
        }
        [Test]
        public void Test_AfterRemove_Count([Values] bool afterRemove)
        {
            var count = 0;
            using var _ = collection.AfterRemove.Subscribe(e => count++);
            (collection as CollectionSerializeField<string>).RemoveAt(0, afterRemove: afterRemove);
            if (afterRemove) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
    }
}