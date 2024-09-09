using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Main
{
    public class UnitTest_Observable
    {
        [Test]
        public void Create()
        {
            int onNext = default;
            var onCompleted = false;
            var onError = false;
            bool onDispose = false;
            IObservable<int> observableResult = null;
            using var observable = Observable.Create<int>(
                (self, observer) =>
                {
                    observer.OnNext(100);
                    observer.OnCompleted();
                    observer.OnError(null);
                    self.Dispose();
                    observableResult = self;
                    return observer;
                },
                self => onDispose = true
                );
            using var _ = observable.Subscribe(
                value => onNext = value,
                () => onCompleted = true,
                error => onError = true
                );
            Assert.AreEqual(observable, observableResult);
            Assert.AreEqual(100, onNext);
            Assert.AreEqual(true, onCompleted);
            Assert.AreEqual(true, onError);
            Assert.AreEqual(true, onDispose);
        }
        [Test]
        public void Return()
        {
            int[] result = new int[] { 1, 2, 3, 4, 5, 6 };
            using var observable = Observable.Return(result);
            int currentIndex = 0;
            using var _ = observable.Subscribe(value =>
            {
                Assert.AreEqual(result[currentIndex], value);
                currentIndex++;
            });
        }
        [Test]
        public void Range([Values(0, 5)] int from, [Values(5, 0)] int to)
        {
            var direction = (to - from) > 0 ? 1 : -1;
            using var observable = Observable.Range(from, to);
            var result = from;
            using var _ = observable.Subscribe(value =>
            {
                Assert.AreEqual(result, value);
                result += direction;
            });
        }
        [Test]
        public void Dispose()
        {
            var observable = Observable.Return(1);
            //
            int observerCount = 0;
            using var _0 = observable.Subscribe(() => observerCount++);
            Assert.AreEqual(1, observerCount);
            //
            observable.Dispose();
            observerCount = 0;
            using var _1 = observable.Subscribe(() => observerCount++);
            Assert.AreEqual(0, observerCount);
            Assert.IsTrue(Reuse.IsInactive(observable));
        }
    }
}