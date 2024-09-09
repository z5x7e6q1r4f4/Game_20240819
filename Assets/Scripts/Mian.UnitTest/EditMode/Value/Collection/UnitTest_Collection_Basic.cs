using NUnit.Framework;
using System.Linq;

namespace Main.Value
{
    public class UnitTest_Collection_Basic : UnitTest_Collection_Base
    {
        [Test]
        public void Add([Values] bool beforeAdd, [Values] bool afterAdd)
        {
            Collection.Add(NewItem, beforeAdd, afterAdd);
            Assert.AreEqual(Items.Count() + 1, Collection.Count);
            Assert.IsTrue(Collection.Contains(NewItem));
        }
        [Test]
        public void AddRange([Values] bool beforeAdd, [Values] bool afterAdd)
        {
            Collection.AddRange(NewItems, beforeAdd, afterAdd);
            Assert.AreEqual(Items.Count() + NewItems.Count(), Collection.Count);
            foreach (var item in NewItems) { Assert.IsTrue(Collection.Contains(item)); }
        }
        [Test]
        public void Insert([Values(-1, 0, 1, 2, 3, 4, 5)] int index, [Values] bool beforeAdd, [Values] bool afterAdd)
        {
            var isError = index > Collection.Count() || index < 0;
            if (isError) { Assert.Catch(() => Collection.Insert(index, NewItem, beforeAdd, afterAdd)); return; }
            Collection.Insert(index, NewItem, beforeAdd, afterAdd);
            Assert.AreEqual(index, Collection.IndexOf(NewItem));
            Assert.AreEqual(NewItem, Collection[index]);
        }
        [Test]
        public void Remove([Values] bool beforeRemove, [Values] bool afterRemove)
        {
            Collection.Remove(Item, beforeRemove, afterRemove);
            Assert.AreEqual(Items.Count() - 1, Collection.Count);
            Assert.IsFalse(Collection.Contains(Item));
        }
        [Test]
        public void RemoveAt([Values(-1, 0, 1, 2, 3)] int index, [Values] bool beforeRemove, [Values] bool afterRemove)
        {
            var isError = index >= Collection.Count() || index < 0;
            if (isError) { Assert.Catch(() => Collection.RemoveAt(index, beforeRemove, afterRemove)); return; }
            Collection.RemoveAt(index, beforeRemove, afterRemove);
            Assert.AreEqual(Items.Count() - 1, Collection.Count);
        }
        [Test]
        public void Clear()
        {
            Collection.Clear();
            Assert.AreEqual(0, Collection.Count);
        }
    }
}