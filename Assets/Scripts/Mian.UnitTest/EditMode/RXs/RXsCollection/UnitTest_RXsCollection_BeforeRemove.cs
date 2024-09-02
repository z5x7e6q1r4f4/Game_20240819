using NUnit.Framework;
using System.Linq;

namespace Main.RXs.RXsCollections
{
    public class UnitTest_RXsCollection_BeforeRemove : UnitTest_RXsCollection_Base
    {
        [Test]
        public void BeforeRemove_Data([ValueSource(nameof(items))] string item)
        {
            var index = collection.IndexOf(item);
            using var _ = collection.BeforeRemove.Subscribe(e =>
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
            using var _ = collection.BeforeRemove.Subscribe(e => count++);
            collection.RemoveAt(0, beforeRemove: beforeRemove);
            if (beforeRemove) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void BeforeRemove_IsEnable([Values] bool isEnable)
        {
            using var _ = collection.BeforeRemove.Subscribe(e => e.IsEnable = isEnable);
            collection.RemoveAt(1);
            if (isEnable) Assert.AreEqual(items.Count() - 1, collection.Count);
            else Assert.AreEqual(items.Count(), collection.Count);
        }
    }
}