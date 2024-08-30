using NUnit.Framework;

namespace Main.RXs .Collection
{
    public class UnitTest_RXsCollection_AfterRemove : UnitTest_RXsCollection_Base 
    {
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