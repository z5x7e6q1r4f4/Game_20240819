using NSubstitute;
using NUnit.Framework;
using System;
namespace Main.RXs
{
    public class UnitTest_Basic
    {
        [Test]
        public void Test_SubscribeToTyped()
        {
            var target = 5;
            var count = 0;
            using var observer = RXsObserver.FromAction<int>(x => { Assert.AreEqual(target, x); count++; });
            using var observable = RXsObservable.FromReturn(target);
            observable.SubscribeToTyped(observer);
            Assert.AreEqual(1, count);
        }
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