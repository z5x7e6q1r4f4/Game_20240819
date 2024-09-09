using NUnit.Framework;
using System.Linq;

namespace Main
{
    public class UnitTest_Reuse
    {
        [Test]
        public void GetPool()
        {
            using var pool = Reuse.GetPool<TestReuseable>();
            var samePool = Reuse.GetPool<TestReuseable>();
            Assert.AreEqual(pool, samePool);
            Assert.AreEqual(0, pool.Inactive.Count());
            //Basic
            Assert.AreEqual(typeof(TestReuseable), pool.Key);
            Assert.IsFalse(pool.IsPrefab);
            //Count
            Assert.AreEqual(0, pool.All.Count());
            Assert.AreEqual(0, pool.Active.Count());
            Assert.AreEqual(0, pool.Active.Count());
        }
        [Test]
        public void Get()
        {
            var reuesable1 = Reuse.Get<TestReuseable>();
            Assert.IsTrue(reuesable1.hasOnGet);
            var reuesable2 = Reuse.Get<TestReuseable>(onGet: false);
            Assert.IsFalse(reuesable2.hasOnGet);
            Assert.AreEqual(reuesable1.Pool, reuesable2.Pool);
            Reuse.Clear();
        }
        [Test]
        public void Release()
        {
            var reuseable1 = Reuse.Get<TestReuseable>();
            reuseable1.ReleaseToReusePool();
            Assert.IsTrue(reuseable1.hasOnRelease);
            var reuseable2 = Reuse.Get<TestReuseable>();
            reuseable2.ReleaseToReusePool(false);
            Assert.IsFalse(reuseable2.hasOnRelease);
            Assert.AreEqual(reuseable1, reuseable2);
        }
        [Test]
        public void Dispose()
        {
            var reuseable1 = Reuse.Get<TestReuseable>();
            var reuseable2 = Reuse.Get<TestReuseable>();
            reuseable2.ReleaseToReusePool();
            Reuse.GetPool<TestReuseable>().Dispose();
            reuseable1.ReleaseToReusePool();
            Assert.IsTrue(reuseable1.hasDestroy);
            Assert.IsTrue(reuseable2.hasDestroy);
        }
        private class TestReuseable :
            IReuseable.IOnGet,
            IReuseable.IOnRelease,
            IReuseable.IOnDestroy
        {
            public bool hasDestroy = false;
            public bool hasOnGet = false;
            public bool hasOnRelease = false;
            public Reuse.IPool Pool { get; set; }
            public void OnDestroy() => hasDestroy = true;
            public void OnGet() { hasOnGet = true; hasOnRelease = false; }
            public void OnRelease() { hasOnRelease = true; hasOnGet = false; }
        }
    }
}