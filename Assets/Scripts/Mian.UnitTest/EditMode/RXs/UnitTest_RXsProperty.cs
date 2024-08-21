using NUnit.Framework;

namespace Main.RXs
{
    public class UnitTest_RXsProperty
    {
        private readonly string newValue = "newValue";
        private readonly string value = "value";
        private RXsProperty_SerializeField<string> property;
        [SetUp]
        public void SetUp()
        {
            property = new(value);
            Assert.AreEqual(value, property.Value);
        }
        [Test]
        public void SetValue([Values] bool beforeSet, [Values] bool afterSet)
        {
            property.SetValue(newValue, beforeSet, afterSet);
            Assert.AreEqual(newValue, property.Value);
            property.Value = value;
            Assert.AreEqual(value, property.Value);
        }
        //Event
        [Test]
        public void BeforeSet_Data()
        {
            property.BeforeSet.Subscribe(e =>
            {
                Assert.AreEqual(property, e.Property);
                Assert.AreEqual(value, e.Previous);
                Assert.AreEqual(newValue, e.Current);
                Assert.AreEqual(true, e.IsEnable);
            });
            property.Value = newValue;
        }
        [Test]
        public void BeforeSet_Count([Values] bool beforeSet)
        {
            var count = 0;
            property.BeforeSet.Subscribe(e => count++);
            property.SetValue(newValue, beforeSet: beforeSet);
        }
        [Test]
        public void BeforeSet_Modified([Values(null, "modified")] string modified)
        {
            var useModified = modified != null;
            property.BeforeSet.Subscribe(e => { if (useModified) e.Modified = modified; });
            property.SetValue(newValue);
            if (useModified) Assert.AreEqual(modified, property.Value);
            else Assert.AreEqual(newValue, property.Value);
        }
        [Test]
        public void BeforeSet_IsEnable([Values] bool isEnable)
        {
            property.BeforeSet.Subscribe(e => e.IsEnable = isEnable);
            property.SetValue(newValue);
            if (isEnable) Assert.AreEqual(newValue, property.Value);
            else Assert.AreEqual(value, property.Value);
        }
        //
        [Test]
        public void AfterSet_Data()
        {
            property.AfterSet.Subscribe(e =>
            {
                Assert.AreEqual(property, e.Property);
                Assert.AreEqual(value, e.Previous);
                Assert.AreEqual(newValue, e.Current);
            });
            property.Value = newValue;
        }
        [Test]
        public void AfterSet_Count([Values] bool afterSet)
        {
            var count = 0;
            property.AfterSet.Subscribe(e => count++);
            property.SetValue(newValue, afterSet: afterSet);
            if (afterSet) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void AfterSet_IsEnable([Values] bool isEnable)
        {
            var count = 0;
            property.BeforeSet.Subscribe(e => e.IsEnable = isEnable);
            property.AfterSet.Subscribe(e => count++);
            property.SetValue(newValue);
            if (isEnable) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void AfterSet_Modified([Values(null, "modified")] string modified)
        {
            var useModified = modified != null;
            property.BeforeSet.Subscribe(e => { if (useModified) e.Modified = modified; });
            property.AfterSet.Subscribe(e =>
            {
                if (useModified) Assert.AreEqual(modified, e.Current);
                else Assert.AreEqual(newValue, e.Current);
            });
            property.SetValue(newValue);
        }
    }
}