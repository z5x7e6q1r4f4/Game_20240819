using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Main.RXs
{
    public class UnitTest_RXsObservable
    {

        [Test]
        public void Test_Base()
        {
            bool isOnNext = false;
            bool isDisposiable = false;
            var observable = Observable.Create<int>(
                (self, observer) =>
                {
                    ((System.IDisposable)self).Dispose();
                    observer.OnNext(1); return observer;
                }, _ => isDisposiable = true);
            using var _ = observable.Subscribe(e =>
            {
                Assert.AreEqual(1, e);
                isOnNext = true;
            });
            Assert.AreEqual(true, isOnNext);
            Assert.AreEqual(true, isDisposiable);
        }
        [Test]
        [TestCaseSource(nameof(Range_Source))]
        public void Test_Range(int from, int to, int expectCount, IEnumerable<int> expectResult)
        {
            List<int> result = new();
            using var range = Observable.Range(from, to);
            using var _ = range.Subscribe(x => result.Add(x));
            //
            Assert.AreEqual(expectCount, result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(expectResult.ElementAt(i), result[i]);
            }
        }
        public static IEnumerable<object[]> Range_Source()
        {
            yield return new object[] { 0, 5, 5, new int[] { 0, 1, 2, 3, 4 } };
            yield return new object[] { 5, 0, 5, new int[] { 5, 4, 3, 2, 1 } };
        }
        [Test]
        [TestCaseSource(nameof(Return_Source))]
        public void Test_FromReturn(IEnumerable<object> input)
        {
            List<object> result = new();
            using var _return = Observable.Return(input);
            using var _ = _return.Subscribe(x => result.Add(x));
            Assert.AreEqual(input.Count(), result.Count);
            for (int i = 0; i < result.Count; i++)
            {
                Assert.AreEqual(input.ElementAt(i), result[i]);
            }
        }
        public static IEnumerable<object[]> Return_Source()
        {
            yield return new object[] { new object[] { "123", 1, .5f, new() } };
            yield return new object[] { new object[] { new(), 37f, 10, "string" } };
        }
    }
}