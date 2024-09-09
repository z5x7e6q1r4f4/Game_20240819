using NUnit.Framework;

namespace Main.Value
{
    public class UnitTest_Collection_AfterRemove : UnitTest_Collection_Base
    {
        [Test]
        public void AfterRemove_Data([ValueSource(nameof(Items))] string item)
        {
            var index = Collection.IndexOf(item);
            using var _ = Collection.AfterRemove.Subscribe(e =>
            {
                Assert.AreEqual(Collection, e.Collection);
                Assert.AreEqual(index, e.Index);
                Assert.AreEqual(item, e.Item);
            });
            Collection.Remove(item);
        }
        [Test]
        public void AfterRemove_Count([Values] bool afterRemove)
        {
            var count = 0;
            using var _ = Collection.AfterRemove.Subscribe(e => count++);
            Collection.RemoveAt(0, afterRemove: afterRemove);
            if (afterRemove) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void AfterRemove_IsEnable([Values] bool isEnable)
        {
            var count = 0;
            using var _1 = Collection.BeforeRemove.Subscribe(e => e.IsEnable = isEnable);
            using var _2 = Collection.AfterRemove.Subscribe(e => count++);
            Collection.RemoveAt(1);
            if (isEnable) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
    }
}