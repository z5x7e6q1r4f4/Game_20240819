using NSubstitute;
using NUnit.Framework;
using System;
namespace Main.RXs
{
    public class UnitTest_RXsObserver
    {
        [Test]
        public void Test_FromAction()
        {
            int nextValue = default;
            bool hasOnNext = default;
            bool hasOnCompleted = default;
            bool hasOnError = default;
            bool hasDispose = default;
            var observer = Observer.Create<int>(
                e => { hasOnNext = true; nextValue = e; },
                () => hasOnCompleted = true,
                e => hasOnError = true,
                () => hasDispose = true
                );
            Assert.AreEqual(default(int), nextValue);
            Assert.AreEqual(false, hasOnNext);
            Assert.AreEqual(false, hasOnCompleted);
            Assert.AreEqual(false, hasOnError);
            Assert.AreEqual(false, hasDispose);
            observer.OnNext(1);
            observer.OnCompleted();
            observer.OnError(null);
            observer.Dispose();
            Assert.AreEqual(1, nextValue);
            Assert.AreEqual(true, hasOnNext);
            Assert.AreEqual(true, hasOnCompleted);
            Assert.AreEqual(true, hasOnError);
            Assert.AreEqual(true, hasDispose);
        }
        [Test]
        public void Test_Reusable()
        {
            var test = TestRXsObserverBaseReuseable.GetFromReusePool();
            (test as IDisposable).Dispose();
            Assert.AreEqual(true, test.isDispose);
        }
        public class TestRXsObserverBaseReuseable : Observer.ObserverBaseReuseable<TestRXsObserverBaseReuseable, object>
        {
            public bool isDispose = false;
            protected override void OnCompleted() { }
            protected override void OnError(Exception error) { }
            protected override void OnNext(object value) { }
            protected override void Dispose()
            {
                isDispose = true;
                base.Dispose();
            }
            public static TestRXsObserverBaseReuseable GetFromReusePool() => GetFromReusePool(false);
        }
    }
}