using NUnit.Framework;
using System;
using System.Linq;

namespace Main
{
    public class UnitTest_Operation
    {
        [Test]
        public void Order()
        {
            int result = 0;
            EventHandler<int> eventHandler = new();
            using var _2 = eventHandler.Order(2).Subscribe(e => Assert.AreEqual(2, result++));
            using var _1 = eventHandler.Order(1).Subscribe(e => Assert.AreEqual(1, result++));
            using var _0 = eventHandler.Order(0).Subscribe(e => Assert.AreEqual(0, result++));
            eventHandler.Invoke(0);
        }
        [Test]
        public void Where()
        {
            Predicate<int> predicate = value => 0 < value && value < 10;
            var observable = new EventHandler<int>();
            using var _ = observable.Where(predicate).Subscribe(value => Assert.IsTrue(predicate(value)));
            observable.Invoke(0);
            observable.Invoke(5);
            observable.Invoke(10);
        }
        [Test]
        public void OfType()
        {
            using var observable = Observable.Return<object>(true, 0, "test", new object());
            using var _1 = observable.OfType<string>().Subscribe(x => Assert.AreEqual("test", x));
            using var _2 = observable.OfType<int>().Subscribe(x => Assert.AreEqual(0, x));
            using var _3 = observable.OfType<bool>().Subscribe(x => Assert.AreEqual(true, x));
        }
        [Test]
        public void Take([Values(0, 1, 2, 3, 4)] int take)
        {
            int result = default;
            using var range = Observable.Range(0, 10);
            var disposable = range.Take(take).Subscribe(value => result = value);
            Assert.AreEqual(Math.Max(0, take - 1), result);
            Assert.IsTrue(Reuse.IsInactive(disposable as IReuseable));
        }
        [Test]
        public void Skip([Values(0, 1, 2, 3, 4)] int skip)
        {
            int value = 100;
            using var range = Observable.Range(0, 10);
            using var _ = range.Skip(skip).Subscribe(e => value = Math.Min(value, e));
            Assert.AreEqual(skip, value);
        }
        [Test]
        public void Select()
        {
            using var _return = Observable.Return(1);
            using var _ = _return.Select(x => x + 1).Subscribe(x => Assert.AreEqual(2, x));
        }
        [Test]
        public void Merge()
        {
            using var observable1 = new EventHandler<int>();
            using var observable2 = new EventHandler<string>();
            using var observable3 = new EventHandler<float>();
            using var observable4 = new EventHandler<double>();
            object result = null;
            using var _ = observable1.Merge(observable2).Merge(observable3).Merge(observable4).
                Subscribe(value => result = value);
            //
            observable1.Invoke(0);
            Assert.AreEqual(0, result);
            //
            observable2.Invoke("string");
            Assert.AreEqual("string", result);
            //
            observable3.Invoke(.0f);
            Assert.AreEqual(.0f, result);
            //
            observable4.Invoke(.1d);
            Assert.AreEqual(.1d, result);
            //Clear(Dispose)
            observable1.Clear();
            observable1.Invoke(1);
            Assert.AreEqual(.1d, result);
            //
            observable3.Clear();
            observable3.Invoke(.1f);
            Assert.AreEqual(.1d, result);
        }
        [Test]
        public void Dispose()
        {
            using var observable = Observable.Return(true, 0, "test", new object());
            var where = observable.Where(x => x is int);
            var ofType = where.OfType<int>();
            var select = ofType.Select(x => x + 1);
            var observer = Observer.Create<int>();
            var sub = select.Subscribe(observer);
            sub.Dispose();
            Assert.IsTrue(Reuse.IsInactive(where as IReuseable));
            Assert.IsTrue(Reuse.IsInactive(ofType as IReuseable));
            Assert.IsTrue(Reuse.IsInactive(select as IReuseable));
            Assert.IsTrue(Reuse.IsInactive(observer));
            Assert.IsTrue(Reuse.IsInactive(sub as IReuseable));
            Assert.AreEqual(0, (observer as IReuseable).Pool.Active.Count());
        }
    }
}