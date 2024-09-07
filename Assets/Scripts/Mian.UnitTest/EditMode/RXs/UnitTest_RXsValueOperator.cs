using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    public class UnitTest_RXsValueOperator
    {
        [Test]
        public void ConnectTo_Collection()
        {
            var collection = new CollectionSerializeField<int> { 1, 2, 3 };
            var result = new CollectionSerializeField<int>();
            var subscription = collection.ConnectTo(result);
            Assert.AreEqual(collection.Count, result.Count);
            for (int i = 0; i < collection.Count; i++)
            {
                Assert.AreEqual(collection[i], result[i]);
            }
            subscription.Dispose();
            Assert.AreEqual(0, result.Count);
        }
        [Test]
        public void ConnectTo_Property()
        {
            var collection = new PropertySerializeField<int>(1);
            var result = new PropertySerializeField<int>();
            var subscription = collection.ConnectTo(result);
            Assert.AreEqual(collection.Value, result.Value);
            subscription.Dispose();
            Assert.AreEqual(0, result.Value);
        }
        [Test]
        public void Where([Values(3, 25)] int from, [Values(68, 12)] int to, [Values(10, 32)] int grater)
        {
            var collection = new CollectionSerializeField<int>();
            using var range = Observable.Range(from, to);
            using var sub = range.Subscribe(e => collection.Add(e));
            using var where = collection.Where(x => x > grater);
            foreach (var x in where)
            { Assert.IsTrue(x > grater); }
            Assert.AreEqual(collection.AsEnumerable().Where(x => x > grater).Count(), where.Count);
        }
        [Test]
        public void OfType()
        {
            OfType_Tester<string>(1, 2, 3, .1f, .2f, .3f, "s1", "s2", "s3");
            OfType_Tester<int>(1, 2, 3, .1f, .2f, .3f, "s1", "s2", "s3");
            OfType_Tester<float>(1, 2, 3, .1f, .2f, .3f, "s1", "s2", "s3");
        }
        public void OfType_Tester<T>(params object[] source)
        {
            var collection = new CollectionSerializeField<object>();
            var typedCollection = new CollectionSerializeField<T>();
            using var subscription = collection.OfType(typedCollection);
            collection.AddRange(source);
            var typedSource = source.OfType<T>();
            for (int i = 0; i < typedCollection.Count; i++)
            {
                Assert.AreEqual(typedSource.ElementAt(i), typedCollection[i]);
            }
            subscription.Dispose();
            Assert.AreEqual(0, typedCollection.Count);
        }
        [Test]
        public void ObserverOn()
        {
            var a = new ClassA();
            var b1 = new ClassB();
            var b2 = new ClassB();
            var b3 = new ClassB();
            var c1 = new ClassC();
            var c2 = new ClassC();
            var c3 = new ClassC();
            //
            b2.ClassC.Value = c2;
            b3.ClassC.Value = c3;
            //
            using var c_Observer = a.ClassB.ObserverOn(b => b.ClassC);
            Assert.AreEqual(null, c_Observer.Value);
            a.ClassB.Value = b1;
            Assert.AreEqual(null, c_Observer.Value);
            b1.ClassC.Value = c1;
            Assert.AreEqual(c1, c_Observer.Value);
            a.ClassB.Value = b2;
            Assert.AreEqual(c2, c_Observer.Value);
            a.ClassB.Value = b3;
            Assert.AreEqual(c3, c_Observer.Value);
        }
        private class ClassA
        { public PropertySerializeField<ClassB> ClassB { get; } = new(); }
        private class ClassB
        { public PropertySerializeField<ClassC> ClassC { get; } = new(); }
        private class ClassC
        { public PropertySerializeField<ClassD> ClassD { get; } = new(); }
        private class ClassD { }
    }
}