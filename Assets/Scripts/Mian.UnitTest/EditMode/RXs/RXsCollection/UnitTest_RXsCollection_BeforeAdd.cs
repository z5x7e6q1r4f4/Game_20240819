using NUnit.Framework;
using System.Linq;

namespace Main.RXs.RXsCollections
{
    public class UnitTest_RXsCollection_BeforeAdd : UnitTest_RXsCollection_Base
    {
        [Test]
        public void BaforeAdd_Data([Values(0, 1, 2)] int index, [Values("a", "b", "c")] string item)
        {
            using var _ = collection.BeforeAdd.Subscribe(e =>
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
            using var _ = collection.BeforeAdd.Subscribe(e => count++);
            collection.Add(newItem, beforeAdd: beforeAdd);
            Assert.AreEqual(beforeAdd ? 1 : 0, count);
        }
        [Test]
        public void BaforeAdd_IsEnable([Values] bool isEnable)
        {
            using var _ = collection.BeforeAdd.Subscribe(e => e.IsEnable = isEnable);
            collection.Add(newItem);
            Assert.AreEqual(isEnable ? items.Count() + 1 : items.Count(), collection.Count);
        }
        [Test]
        public void BaforeAdd_Modified([Values(null, "modified")] string modified)
        {
            var useModified = modified != null;
            using var _ = collection.BeforeAdd.Subscribe(e => { if (useModified) e.Modified = modified; });
            collection.Add(newItem);
            Assert.AreEqual(useModified ? modified : newItem, collection[collection.Count - 1]);
        }
    }
}