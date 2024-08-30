using NUnit.Framework;
using System;
using UnityEngine;

namespace Main.RXs
{
    public class UnitTest_RXsOperator
    {
        [Test]
        public void Order()
        {
            bool result = false;
            RXsEventHandler<int> eventHandler = new();
            eventHandler.Order(2).Subscribe(e =>
            {
                Assert.IsFalse(result);
                result = true;
            });
            eventHandler.Order(1).Subscribe(e =>
            {
                Assert.IsTrue(result);
                result = false;
            });
            eventHandler.Order(0).Subscribe(e =>
            {
                Assert.IsFalse(result);
                result = true;
            });
        }
        [Test]
        public void Where(
            [Values(0, 39)] int from,
            [Values(27, 44)] int to,
            [Values(2, 30)] int grater,
            [Values(7, 15)] int smaller)
        {
            Reuse.Clear();
            var range = Observable.Range(from, to);
            //way1
            var way1Count = 0;
            range.Where(x => x > grater).Where(x => x < smaller).Subscribe(x =>
           {
               Assert.IsTrue(x > grater);
               Assert.IsTrue(x < smaller);
               way1Count++;
           });
            //way2
            var way2Count = 0;
            var flow = from x in range
                       where x > grater
                       where x < smaller
                       select x;
            flow.Subscribe(x =>
            {
                Assert.IsTrue(x > grater);
                Assert.IsTrue(x < smaller);
                way2Count++;
            });
            //
            var dir = to - from >= 0 ? 1 : -1;
            to -= dir;
            var _from = Math.Min(from, to);
            var _to = Math.Max(from, to);

            var resultFrom = Math.Max(_from, grater + 1);
            var resultTo = Math.Min(_to, smaller - 1);

            var expectCount = Math.Max(0, (resultTo - resultFrom) + 1);
            Assert.AreEqual(expectCount, way1Count);
            Assert.AreEqual(expectCount, way2Count);
        }
        [Test]
        public void OfType()
        {
            var observable = Observable.Return<object>(true, 0, "test", new object());
            observable.OfType<string>().Subscribe(x => Assert.AreEqual("test", x));
            observable.OfType<int>().Subscribe(x => Assert.AreEqual(0, x));
            observable.OfType<bool>().Subscribe(x => Assert.AreEqual(true, x));
        }
        [Test]
        public void Select()
        {
            Observable.Return(1).Select(x => x + 1).Subscribe(x => Assert.AreEqual(2, x));
        }
        [Test]
        public void Dispose()
        {
            Reuse.Clear();
            var subscription =
                Observable.Return<object>(true, 0, "test", new object()).
                Where(x => x is int).
                OfType<int>().
                Select(x => x + 1).
                Subscribe(() => { });
            Debug.Log(subscription);
            Reuse.Log();
            subscription.Dispose();
            Reuse.Log();
        }
    }
}