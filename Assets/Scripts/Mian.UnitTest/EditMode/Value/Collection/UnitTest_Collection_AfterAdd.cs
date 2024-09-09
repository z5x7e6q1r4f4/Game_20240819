using NUnit.Framework;

namespace Main.Value
{
    public class UnitTest_Collection_AfterAdd : UnitTest_Collection_Base
    {
        [Test]
        public void AfterAdd_Data([Values(0, 1, 2)] int index, [Values("a", "b", "c")] string item)
        {
            using var _ = Collection.AfterAdd.Subscribe(e =>
            {
                Assert.AreEqual(Collection, e.Collection);
                Assert.AreEqual(index, e.Index);
                Assert.AreEqual(item, e.Item);
            });
            Collection.Insert(index, item);
        }
        [Test]
        public void AfterAdd_Count([Values] bool afterAdd)
        {
            var count = 0;
            using var _ = Collection.AfterAdd.Subscribe(e => count++);
            Collection.Add(NewItem, afterAdd: afterAdd);
            if (afterAdd) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void AfterAdd_Immediately()
        {
            var count = 0;
            using var _ = Collection.AfterAdd.Immediately().Subscribe(e => count++);
            Assert.AreEqual(count, Collection.Count);
        }
        [Test]
        public void AfterAdd_IsEnable([Values] bool isEnable)
        {
            var count = 0;
            using var _1 = Collection.BeforeAdd.Subscribe(e => e.IsEnable = isEnable);
            using var _2 = Collection.AfterAdd.Subscribe(e => count++);
            Collection.Add(NewItem);
            if (isEnable) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void Test_AfterAdd_Modified([Values(null, "modified")] string modified)
        {
            var useModified = modified != null;
            int count = 0;
            using var _1 = Collection.BeforeAdd.Subscribe(e => { if (useModified) e.Modified = modified; });
            using var _2 = Collection.AfterAdd.Subscribe(e =>
            {
                count++;
                if (useModified) Assert.AreEqual(modified, e.Item);
                else Assert.AreEqual(NewItem, e.Item);
            });
            Collection.Add(NewItem);
            Assert.AreEqual(1, count);
        }
    }
}