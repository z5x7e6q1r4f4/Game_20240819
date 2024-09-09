using NUnit.Framework;
using System.Linq;

namespace Main.Value
{
    public class UnitTest_Collection_BeforeAdd : UnitTest_Collection_Base
    {
        [Test]
        public void BaforeAdd_Data([Values(0, 1, 2)] int index, [Values("a", "b", "c")] string item)
        {
            using var _ = Collection.BeforeAdd.Subscribe(e =>
             {
                 Assert.AreEqual(Collection, e.Collection);
                 Assert.AreEqual(index, e.Index);
                 Assert.AreEqual(item, e.Item);
                 Assert.AreEqual(item, e.Modified);
             });
            Collection.Insert(index, item);
        }
        [Test]
        public void BaforeAdd_Count([Values] bool beforeAdd)
        {
            var count = 0;
            using var _ = Collection.BeforeAdd.Subscribe(e => count++);
            Collection.Add(NewItem, beforeAdd: beforeAdd);
            Assert.AreEqual(beforeAdd ? 1 : 0, count);
        }
        [Test]
        public void BaforeAdd_IsEnable([Values] bool isEnable)
        {
            using var _ = Collection.BeforeAdd.Subscribe(e => e.IsEnable = isEnable);
            Collection.Add(NewItem);
            Assert.AreEqual(isEnable ? Items.Count() + 1 : Items.Count(), Collection.Count);
        }
        [Test]
        public void BaforeAdd_Modified([Values(null, "modified")] string modified)
        {
            var useModified = modified != null;
            using var _ = Collection.BeforeAdd.Subscribe(e => { if (useModified) e.Modified = modified; });
            Collection.Add(NewItem);
            Assert.AreEqual(useModified ? modified : NewItem, Collection[Collection.Count - 1]);
        }
    }
}