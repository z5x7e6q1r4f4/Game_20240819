using NUnit.Framework;
using System;
namespace Main
{
    public class UnitTest_Observer
    {
        [Test]
        public void Create()
        {
            int onNext = default;
            bool onError = false;
            bool onCompleted = false;
            bool onDispose = false;
            IObserver<int> observerResult = null;
            var observer = Observer.Create<int>(
               (obs, value) => { onNext = value; observerResult = obs; },
                obs => { onCompleted = true; observerResult = obs; },
                (obs, error) => { onError = true; observerResult = obs; },
                obs => { onDispose = true; observerResult = obs as IObserver<int>; }
                );
            var observerList = new ObserverList<int>();
            using var _ = observerList.Subscribe(observer);
            observerList.OnNext(100);
            Assert.AreEqual(100, onNext);
            Assert.AreEqual(observer, observerResult);
            observerList.OnCompleted();
            Assert.AreEqual(true, onCompleted);
            Assert.AreEqual(observer, observerResult);
            observerList.OnError(null);
            Assert.AreEqual(true, onError);
            Assert.AreEqual(observer, observerResult);
            observer.Dispose();
            Assert.AreEqual(true, onDispose);
            Assert.AreEqual(observer, observerResult);
        }
        [Test]
        public void ToDisposableHandler()
        {
            using var observer = Observer.Create<int>();
            Assert.AreEqual(observer, observer.AsObserverBase());
        }
        [Test]
        public void Subscribe()
        {
            int onNext = default;
            bool onError = false;
            bool onCompleted = false;
            var observerList = new ObserverList<int>();
            using var _ = observerList.Subscribe(
               value => onNext = value,
                () => onCompleted = true,
                 error => onError = true
                );
            observerList.OnNext(100);
            Assert.AreEqual(100, onNext);
            observerList.OnCompleted();
            Assert.AreEqual(true, onCompleted);
            observerList.OnError(null);
            Assert.AreEqual(true, onError);
        }
        [Test]
        public void Dispose()
        {
            var observable = new EventHandler<int>();
            //Dispose subscription
            var observer = Observer.Create<int>();
            var subscription = observable.Subscribe(observer);
            Assert.IsTrue(Reuse.IsActive(observer));
            Assert.IsTrue(Reuse.IsActive(subscription as IReuseable));
            subscription.Dispose();
            Assert.IsTrue(Reuse.IsInactive(observer));
            Assert.IsTrue(Reuse.IsInactive(subscription as IReuseable));
            //Dispose observer
            observer = Observer.Create<int>();
            subscription = observable.Subscribe(observer);
            Assert.IsTrue(Reuse.IsActive(observer));
            Assert.IsTrue(Reuse.IsActive(subscription as IReuseable));
            observer.Dispose();
            Assert.IsTrue(Reuse.IsInactive(observer));
            Assert.IsTrue(Reuse.IsInactive(subscription as IReuseable));
            //Clear observable
            observer = Observer.Create<int>();
            subscription = observable.Subscribe(observer);
            Assert.IsTrue(Reuse.IsActive(observer));
            Assert.IsTrue(Reuse.IsActive(subscription as IReuseable));
            observable.Clear();
            Assert.IsTrue(Reuse.IsInactive(observer));
            Assert.IsTrue(Reuse.IsInactive(subscription as IReuseable));
        }
    }
}