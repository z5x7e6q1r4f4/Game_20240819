using NUnit.Framework;
using System;

namespace Main.RXs
{
    public class UnitTest_RXsOperator
    {
        [Test]
        public void Test_Where(
            [Values(0, 39)] int from,
            [Values(27, 44)] int to,
            [Values(2, 30)] int grater,
            [Values(7, 15)] int smaller)
        {
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
    }
}