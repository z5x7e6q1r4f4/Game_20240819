using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Main.Value
{
    public abstract class UnitTest_Collection_Base
    {
        protected static IEnumerable<string> Items
        {
            get
            {
                yield return "Item1";
                yield return "Item2";
                yield return "Item3";
            }
        }
        protected static IEnumerable<string> NewItems
        {
            get
            {
                yield return "NewItem1";
                yield return "NewItem2";
                yield return "NewItem3";
            }
        }
        protected static string Item => Items.First();
        protected static string NewItem => NewItems.First();
        protected CollectionSerializeField<string> Collection { get; private set; }
        [SetUp]
        public void SetUp()
        {
            Collection = new(Items);
            Assert.AreEqual(Items.Count(), Collection.Count);
            foreach (var item in Items)
            {
                Assert.True(Collection.Contains(item));
            }
        }
    }
}