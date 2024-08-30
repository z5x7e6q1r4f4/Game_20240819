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
            var observer = Substitute.For<IObserver>();
            var observable = Substitute.For<IRXsObservable<int>>();
            observable.SubscribeToTyped<int>(observer);
            observable.Received(1).Subscribe(Arg.Any<IObserver<int>>());
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
    }
}