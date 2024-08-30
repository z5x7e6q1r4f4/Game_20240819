using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Main.RXs.Collection
{
    public abstract class UnitTest_RXsCollection_Base
    {
        protected static IEnumerable<string> items
        {
            get
            {
                yield return "Item1";
                yield return "Item2";
                yield return "Item3";
            }
        }
        protected static IEnumerable<string> newItems
        {
            get
            {
                yield return "NewItem1";
                yield return "NewItem2";
                yield return "NewItem3";
            }
        }
        protected static string item => items.First();
        protected static string newItem => newItems.First();
        protected RXsCollection_SerializeField<string> collection;
        [SetUp]
        public void SetUp()
        {
            Reuse.Clear();
            collection = new(items);
            Assert.AreEqual(items.Count(), collection.Count);
            foreach (var item in items)
            {
                Assert.True(collection.Contains(item));
            }
        }
    }
}