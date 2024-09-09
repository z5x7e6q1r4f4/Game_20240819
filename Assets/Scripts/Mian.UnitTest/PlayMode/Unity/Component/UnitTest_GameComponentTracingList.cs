using NUnit.Framework;

namespace Main
{
    public class UnitTest_GameComponentTracingList
    {
        [Test]
        public void AddComponent()
        {
            GameComponent result = null;
            var a = Objects.New<TestGameComponentA>();
            Assert.IsTrue(a.GameComponentList.Contains(a));
            using var _ = a.GameComponentList.AfterAdd.Immediately().Subscribe(e => result = e.Item);
            Assert.AreEqual(a, result);
            //
            var b = a.AddComponent<TestGameComponentB>();
            Assert.IsTrue(a.GameComponentList.Contains(b));
            Assert.AreEqual(b, result);
            //
            var c = a.AddComponent<TestGameComponentC>(isTrackable: false);
            Assert.IsFalse(a.GameComponentList.Contains(c));
            Assert.AreEqual(b, result);
        }
        [Test]
        public void RemoveComponent()
        {
            GameComponent result = null;
            var a = Objects.New<TestGameComponentA>();
            var b = a.AddComponent<TestGameComponentB>();
            var c = a.AddComponent<TestGameComponentC>(isTrackable: false);
            //
            Assert.IsTrue(a.GameComponentList.Contains(a));
            Assert.IsTrue(a.GameComponentList.Contains(b));
            using var _ = a.GameComponentList.AfterRemove.Subscribe(e => result = e.Item);
            //
            b.RemoveComponent();
            Assert.AreEqual(b, result);
            //
            c.RemoveComponent();
            Assert.AreEqual(b, result);
        }
        private class TestGameComponentA : GameComponent { }
        private class TestGameComponentB : GameComponent { }
        private class TestGameComponentC : GameComponent { }
    }
}