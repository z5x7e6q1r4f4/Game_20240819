using NUnit.Framework;

namespace Main.RXs.Collection
{
    public class UnitTest_RXsCollection_AfterAdd : UnitTest_RXsCollection_Base 
    {
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
    }
}