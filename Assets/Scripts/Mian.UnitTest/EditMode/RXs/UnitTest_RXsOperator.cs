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
            using var _2 = eventHandler.Order(2).Subscribe(e =>
                {
                    Assert.IsFalse(result);
                    result = true;
                });
            using var _1 = eventHandler.Order(1).Subscribe(e =>
            {
                Assert.IsTrue(result);
                result = false;
            });
            using var _0 = eventHandler.Order(0).Subscribe(e =>
            {
                Assert.IsFalse(result);
                result = true;
            });
            eventHandler.Invoke(0);
        }
        [Test]
        public void Where(
            [Values(0, 39)] int from,
            [Values(27, 44)] int to,
            [Values(2, 30)] int grater,
            [Values(7, 15)] int smaller)
        {
            Reuse.Clear();
            using var range = RXsObservable.FromRange(from, to);
            //way1
            var way1Count = 0;
            using var _1 = range.Where(x => x > grater).Where(x => x < smaller).Subscribe(x =>
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
            using var _2 = flow.Subscribe(x =>
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
            using var observable = RXsObservable.FromReturn<object>(true, 0, "test", new object());
            using var _1 = observable.OfType<string>().Subscribe(x => Assert.AreEqual("test", x));
            using var _2 = observable.OfType<int>().Subscribe(x => Assert.AreEqual(0, x));
            using var _3 = observable.OfType<bool>().Subscribe(x => Assert.AreEqual(true, x));
        }
        [Test]
        public void Select()
        {
            using var _return = RXsObservable.FromReturn(1);
            using var _ = _return.Select(x => x + 1).Subscribe(x => Assert.AreEqual(2, x));
        }
        [Test]
        public void Dispose()
        {
            using var _return = RXsObservable.FromReturn<object>(true, 0, "test", new object());
            var sub = _return.
                Where(x => x is int).
                OfType<int>().
                Select(x => x + 1).
                Subscribe(() => { });
            sub.Dispose();
        }
    }
}