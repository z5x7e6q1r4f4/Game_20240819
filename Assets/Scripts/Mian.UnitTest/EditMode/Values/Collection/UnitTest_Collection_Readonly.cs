using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Main.Value
{
    public class UnitTest_Collection_Readonly : UnitTest_Collection_Base
    {
        private new ICollectionReadonly<string> Collection => base.Collection;
        [Test]
        public void AfterAdd_Data([Values(0, 1, 2)] int index, [Values("a", "b", "c")] string item)
        {
            using var _ = Collection.AfterAdd.Subscribe(e =>
                {
                    Assert.AreEqual(Collection, e.Collection);
                    Assert.AreEqual(index, e.Index);
                    Assert.AreEqual(item, e.Item);
                });
            (Collection as CollectionSerializeField<string>).Insert(index, item);
        }
        [Test]
        public void AfterAdd_Count([Values] bool afterAdd)
        {
            var count = 0;
            using var _ = Collection.AfterAdd.Subscribe(e => count++);
            (Collection as CollectionSerializeField<string>).Add(NewItem, afterAdd: afterAdd);
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
        public void AfterRemove_Data([ValueSource(nameof(Items))] string item)
        {
            var index = Collection.IndexOf(item);
            using var _ = Collection.AfterRemove.Subscribe(e =>
            {
                Assert.AreEqual(Collection, e.Collection);
                Assert.AreEqual(index, e.Index);
                Assert.AreEqual(item, e.Item);
            });
            (Collection as CollectionSerializeField<string>).Remove(item);
        }
        [Test]
        public void AfterRemove_Count([Values] bool afterRemove)
        {
            var count = 0;
            using var _ = Collection.AfterRemove.Subscribe(e => count++);
            (Collection as CollectionSerializeField<string>).RemoveAt(0, afterRemove: afterRemove);
            if (afterRemove) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
    }
}