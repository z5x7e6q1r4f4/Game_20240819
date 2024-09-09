using NUnit.Framework;

namespace Main.Value
{
    public class UnitTest_Property
    {
        private string NewValue { get; } = "newValue";
        private string Value { get; } = "value";
        private PropertySerializeField<string> Property { get; set; }
        [SetUp]
        public void SetUp()
        {
            Property = new(Value);
            Assert.AreEqual(Value, Property.Value);
        }
        [Test]
        public void SetValue([Values] bool beforeSet, [Values] bool afterSet)
        {
            Property.SetValue(NewValue, beforeSet, afterSet);
            Assert.AreEqual(NewValue, Property.Value);
            Property.Value = Value;
            Assert.AreEqual(Value, Property.Value);
        }
        //Event
        [Test]
        public void BeforeSet_Data()
        {
            using var _ = Property.BeforeSet.Subscribe(e =>
              {
                  Assert.AreEqual(Property, e.Property);
                  Assert.AreEqual(Value, e.Previous);
                  Assert.AreEqual(NewValue, e.Current);
                  Assert.AreEqual(true, e.IsEnable);
              });
            Property.Value = NewValue;
        }
        [Test]
        public void BeforeSet_Count([Values] bool beforeSet)
        {
            var count = 0;
            using var _ = Property.BeforeSet.Subscribe(e => count++);
            Property.SetValue(NewValue, beforeSet: beforeSet);
        }
        [Test]
        public void BeforeSet_Modified([Values(null, "modified")] string modified)
        {
            var useModified = modified != null;
            using var _ = Property.BeforeSet.Subscribe(e => { if (useModified) e.Modified = modified; });
            Property.SetValue(NewValue);
            if (useModified) Assert.AreEqual(modified, Property.Value);
            else Assert.AreEqual(NewValue, Property.Value);
        }
        [Test]
        public void BeforeSet_IsEnable([Values] bool isEnable)
        {
            using var _ = Property.BeforeSet.Subscribe(e => e.IsEnable = isEnable);
            Property.SetValue(NewValue);
            if (isEnable) Assert.AreEqual(NewValue, Property.Value);
            else Assert.AreEqual(Value, Property.Value);
        }
        //
        [Test]
        public void AfterSet_Data()
        {
            using var _ = Property.AfterSet.Subscribe(e =>
            {
                Assert.AreEqual(Property, e.Property);
                Assert.AreEqual(Value, e.Previous);
                Assert.AreEqual(NewValue, e.Current);
            });
            Property.Value = NewValue;
        }
        [Test]
        public void AfterSet_Count([Values] bool afterSet)
        {
            var count = 0;
            using var _ = Property.AfterSet.Subscribe(e => count++);
            Property.SetValue(NewValue, afterSet: afterSet);
            if (afterSet) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void AfterSet_IsEnable([Values] bool isEnable)
        {
            var count = 0;
            using var _1 = Property.BeforeSet.Subscribe(e => e.IsEnable = isEnable);
            using var _2 = Property.AfterSet.Subscribe(e => count++);
            Property.SetValue(NewValue);
            if (isEnable) Assert.AreEqual(1, count);
            else Assert.AreEqual(0, count);
        }
        [Test]
        public void AfterSet_Modified([Values(null, "modified")] string modified)
        {
            var useModified = modified != null;
            using var _1 = Property.BeforeSet.Subscribe(e => { if (useModified) e.Modified = modified; });
            using var _2 = Property.AfterSet.Subscribe(e =>
            {
                if (useModified) Assert.AreEqual(modified, e.Current);
                else Assert.AreEqual(NewValue, e.Current);
            });
            Property.SetValue(NewValue);
        }
    }
}