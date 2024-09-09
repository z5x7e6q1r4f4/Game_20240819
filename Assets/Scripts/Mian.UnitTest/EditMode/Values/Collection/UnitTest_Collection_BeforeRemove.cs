using NUnit.Framework;
using System.Linq;

namespace Main.Value
{
    public class UnitTest_Collection_BeforeRemove : UnitTest_Collection_Base
    {
        [Test]
        public void BeforeRemove_Data([ValueSource(nameof(Items))] string item)
        {
            var index = Collection.IndexOf(item);
            using var _ = Collection.BeforeRemove.Subscribe(e =>
                  {
                      Assert.AreEqual(Collection, e.Collection);
                      Assert.AreEqual(index, e.Index);
                      Assert.AreEqual(item, e.Item);
                      Assert.IsTrue(e.IsEnable);
                  });
            Collection.Remove(item);
        }
        [Test]
        public void BeforeRemove_Count([Values] bool beforeRemove)
        {
            var count = 0;
            using var _ = Collection.BeforeRemove.Subscribe(e => count++);
            Collection.RemoveAt(0, beforeRemove: beforeRemove);
            if (beforeRemove) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void BeforeRemove_IsEnable([Values] bool isEnable)
        {
            using var _ = Collection.BeforeRemove.Subscribe(e => e.IsEnable = isEnable);
            Collection.RemoveAt(1);
            if (isEnable) Assert.AreEqual(Items.Count() - 1, Collection.Count);
            else Assert.AreEqual(Items.Count(), Collection.Count);
        }
    }
}