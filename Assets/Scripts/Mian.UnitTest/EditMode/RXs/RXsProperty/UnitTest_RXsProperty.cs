using NUnit.Framework;

namespace Main.RXs.RXsProperty
{
    public class UnitTest_RXsProperty
    {
        private readonly string newValue = "newValue";
        private readonly string value = "value";
        private PropertySerializeField<string> property;
        [SetUp]
        public void SetUp()
        {
            property = new(value);
            Assert.AreEqual(value, property.Value);
        }
        [Test]
        public void Test_SetValue([Values] bool beforeSet, [Values] bool afterSet)
        {
            property.SetValue(newValue, beforeSet, afterSet);
            Assert.AreEqual(newValue, property.Value);
            property.Value = value;
            Assert.AreEqual(value, property.Value);
        }
        //Event
        [Test]
        public void Test_BeforeSet_Data()
        {
            using var _ = property.BeforeSet.Subscribe(e =>
              {
                  Assert.AreEqual(property, e.Property);
                  Assert.AreEqual(value, e.Previous);
                  Assert.AreEqual(newValue, e.Current);
                  Assert.AreEqual(true, e.IsEnable);
              });
            property.Value = newValue;
        }
        [Test]
        public void Test_BeforeSet_Count([Values] bool beforeSet)
        {
            var count = 0;
            using var _ = property.BeforeSet.Subscribe(e => count++);
            property.SetValue(newValue, beforeSet: beforeSet);
        }
        [Test]
        public void Test_BeforeSet_Modified([Values(null, "modified")] string modified)
        {
            var useModified = modified != null;
            using var _ = property.BeforeSet.Subscribe(e => { if (useModified) e.Modified = modified; });
            property.SetValue(newValue);
            if (useModified) Assert.AreEqual(modified, property.Value);
            else Assert.AreEqual(newValue, property.Value);
        }
        [Test]
        public void Test_BeforeSet_IsEnable([Values] bool isEnable)
        {
            using var _ = property.BeforeSet.Subscribe(e => e.IsEnable = isEnable);
            property.SetValue(newValue);
            if (isEnable) Assert.AreEqual(newValue, property.Value);
            else Assert.AreEqual(value, property.Value);
        }
        //
        [Test]
        public void Test_AfterSet_Data()
        {
            using var _ = property.AfterSet.Subscribe(e =>
            {
                Assert.AreEqual(property, e.Property);
                Assert.AreEqual(value, e.Previous);
                Assert.AreEqual(newValue, e.Current);
            });
            property.Value = newValue;
        }
        [Test]
        public void Test_AfterSet_Count([Values] bool afterSet)
        {
            var count = 0;
            using var _ = property.AfterSet.Subscribe(e => count++);
            property.SetValue(newValue, afterSet: afterSet);
            if (afterSet) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void Test_AfterSet_IsEnable([Values] bool isEnable)
        {
            var count = 0;
            using var _1 = property.BeforeSet.Subscribe(e => e.IsEnable = isEnable);
            using var _2 = property.AfterSet.Subscribe(e => count++);
            property.SetValue(newValue);
            if (isEnable) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void Test_AfterSet_Modified([Values(null, "modified")] string modified)
        {
            var useModified = modified != null;
            using var _1 = property.BeforeSet.Subscribe(e => { if (useModified) e.Modified = modified; });
            using var _2 = property.AfterSet.Subscribe(e =>
            {
                if (useModified) Assert.AreEqual(modified, e.Current);
                else Assert.AreEqual(newValue, e.Current);
            });
            property.SetValue(newValue);
        }
    }
}