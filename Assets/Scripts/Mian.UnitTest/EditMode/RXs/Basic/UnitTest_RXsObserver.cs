using NSubstitute;
using NUnit.Framework;
using System;
namespace Main.RXs
{
    public class UnitTest_RXsObserver
    {
        [Test]
        public void Test_OnNextToTyped()
        {
            var observer = Substitute.For<IRXsObserver<int>>();
            observer.OnNextToTyped<int>(0);
            observer.Received(1).OnNext(0);
        }
        [Test]
        public void Test_OnCompletedToTyped()
        {
            var observer = Substitute.For<IRXsObserver<int>>();
            observer.OnCompletedToTyped<int>();
            (observer as IObserver<int>).Received(1).OnCompleted();
        }
        [Test]
        public void Test_OnErrorToTyped()
        {
            var observer = Substitute.For<IRXsObserver<int>>();
            observer.OnErrorToTyped<int>(null);
            (observer as IObserver<int>).Received(1).OnError(null);
        }
        [Test]
        public void Test_FromAction()
        {
            int nextValue = default;
            bool hasOnNext = default;
            bool hasOnCompleted = default;
            bool hasOnError = default;
            bool hasDispose = default;
            var observer = RXsObserver.FromAction<int>(
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
            observer.OnCompletedToTyped<int>();
            observer.OnErrorToTyped<int>(null);
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
            Assert.AreEqual(true, test.isRelease);
        }
        public class TestRXsObserverBaseReuseable : RXsObserverBaseReusable<TestRXsObserverBaseReuseable, object>
        {
            public bool isRelease = false;
            protected override void OnCompleted() { }
            protected override void OnError(Exception error) { }
            protected override void OnNext(object value) { }
            protected override void OnRelease()
            {
                isRelease = true;
                base.OnRelease();
            }
            public new static TestRXsObserverBaseReuseable GetFromReusePool()
                => RXsObserverBaseReusable<TestRXsObserverBaseReuseable, object>.GetFromReusePool();
        }
    }
}