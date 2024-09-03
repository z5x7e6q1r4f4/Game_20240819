using NUnit.Framework;
using System.Linq;

namespace Main.RXs.RXsCollections
{
    public class UnitTest_RXsCollection_Basic : UnitTest_RXsCollection_Base
    {
        [Test]
        public void Test_Add([Values] bool beforeAdd, [Values] bool afterAdd)
        {
            collection.Add(newItem, beforeAdd, afterAdd);
            Assert.AreEqual(items.Count() + 1, collection.Count);
            Assert.IsTrue(collection.Contains(newItem));
        }
        [Test]
        public void Test_AddRange([Values] bool beforeAdd, [Values] bool afterAdd)
        {
            collection.AddRange(newItems, beforeAdd, afterAdd);
            Assert.AreEqual(items.Count() + newItems.Count(), collection.Count);
            foreach (var item in newItems) { Assert.IsTrue(collection.Contains(item)); }
        }
        [Test]
        public void Test_Insert([Values(-1, 0, 1, 2, 3, 4, 5)] int index, [Values] bool beforeAdd, [Values] bool afterAdd)
        {
            var isError = index > collection.Count() || index < 0;
            if (isError) { Assert.Catch(() => collection.Insert(index, newItem, beforeAdd, afterAdd)); return; }
            collection.Insert(index, newItem, beforeAdd, afterAdd);
            Assert.AreEqual(index, collection.IndexOf(newItem));
            Assert.AreEqual(newItem, collection[index]);
        }
        [Test]
        public void Test_Remove([Values] bool beforeRemove, [Values] bool afterRemove)
        {
            collection.Remove(item, beforeRemove, afterRemove);
            Assert.AreEqual(items.Count() - 1, collection.Count);
            Assert.IsFalse(collection.Contains(item));
        }
        [Test]
        public void Test_RemoveAt([Values(-1, 0, 1, 2, 3)] int index, [Values] bool beforeRemove, [Values] bool afterRemove)
        {
            var isError = index >= collection.Count() || index < 0;
            if (isError) { Assert.Catch(() => collection.RemoveAt(index, beforeRemove, afterRemove)); return; }
            collection.RemoveAt(index, beforeRemove, afterRemove);
            Assert.AreEqual(items.Count() - 1, collection.Count);
        }
        [Test]
        public void Test_Clear()
        {
            collection.Clear();
            Assert.AreEqual(0, collection.Count);
        }
    }
}