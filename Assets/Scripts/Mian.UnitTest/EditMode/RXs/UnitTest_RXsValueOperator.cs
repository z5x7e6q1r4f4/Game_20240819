using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    public class UnitTest_RXsValueOperator
    {
        [Test]
        public void Where([Values(3, 25)] int from, [Values(68, 12)] int to, [Values(10, 32)] int grater)
        {
            var collection = new RXsCollection_SerializeField<int>();
            Observable.Range(from, to).Subscribe(e => collection.Add(e));
            var where = collection.Where(x => x > grater);
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
            var collection = new RXsCollection_SerializeField<object>();
            collection.EnableDebug("Collection");
            var typedCollection = new RXsCollection_SerializeField<T>();
            typedCollection.EnableDebug("TypedCollection");
            var subscription = collection.OfType(typedCollection);
            collection.AddRange(source);
            var typedSource = source.OfType<T>();
            for (int i = 0; i < typedCollection.Count; i++)
            {
                UnityEngine.Debug.Log($"TestType : {typeof(T)} , Value : {typedCollection[i]} , ValueType : {typedCollection[i].GetType()}");
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
            var c_Observer = a.ClassB.ObserverOn(b => b.ClassC);
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
        { public RXsProperty_SerializeField<ClassB> ClassB { get; } = new(); }
        private class ClassB
        { public RXsProperty_SerializeField<ClassC> ClassC { get; } = new(); }
        private class ClassC
        { public RXsProperty_SerializeField<ClassD> ClassD { get; } = new(); }
        private class ClassD { }
    }
}