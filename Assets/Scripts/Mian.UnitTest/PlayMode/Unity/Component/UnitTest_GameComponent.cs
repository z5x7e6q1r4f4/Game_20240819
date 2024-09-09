using NUnit.Framework;
using System.Collections;
using UnityEngine.TestTools;

namespace Main
{
    public class UnitTest_GameComponent
    {
        [Test]
        public void Awake()
        {
            bool result = false;
            var gameComponent = Objects.New<TestGameComponent>();
            Assert.AreEqual(1, gameComponent.AwakeCount);
            var temp = gameComponent.SomeValueForOutside;
            Assert.AreEqual(1, gameComponent.AwakeCount);
            gameComponent.OnGameComponentAwakeEvent.Immediately().Subscribe(() => result = true);
            Assert.IsTrue(result);
        }
        [Test]
        public void EnableAndDisable()
        {
            bool result = false;
            var gameComponent = Objects.New<TestGameComponent>();
            //Normal
            var _1 = gameComponent.OnGameComponentEnableEvent.Subscribe(() => result = true);
            var _2 = gameComponent.OnGameComponentDisableEvent.Subscribe(() => result = false);
            Assert.AreEqual(true, gameComponent.IsGameComponentEnable);
            Assert.AreEqual(false, result);
            result = true;
            gameComponent.enabled = false;
            Assert.AreEqual(false, gameComponent.IsGameComponentEnable);
            Assert.AreEqual(false, result);
            _1.Dispose();
            _2.Dispose();
            //Immediately
            gameComponent.enabled = true;
            result = false;
            gameComponent.OnGameComponentEnableEvent.Immediately().Subscribe(x => result = true).Dispose();
            Assert.IsTrue(result);
            gameComponent.enabled = false;
            result = true;
            gameComponent.OnGameComponentDisableEvent.Immediately().Subscribe(x => result = false).Dispose();
            Assert.IsFalse(result);
        }
        [Test]
        public void Destory()
        {
            bool result = false;
            var gameComponent = Objects.New<TestGameComponent>();
            gameComponent.OnGameComponentDestroyEvent.Subscribe(() => result = true);
            gameComponent.DestroyImmediate();
            Assert.IsTrue(gameComponent.IsGameComponentDestroy);
            Assert.IsTrue(result);
        }
        private class TestGameComponent : GameComponent, IMonoBehaviourTest
        {
            public bool IsGameComponentDestroy { get; private set; } = false;
            public bool IsGameComponentEnable { get; private set; } = false;
            public int SomeValueForOutside => AwakeSelf<TestGameComponent>().someValueForOutside;
            public bool IsTestFinished { get; private set; }

            private int someValueForOutside;
            public int AwakeCount = 0;
            protected override void OnGameComponentAwake()
            {
                someValueForOutside = 100;
                AwakeCount++;
            }
            protected override void OnGameComponentDestroy()
            {
                IsGameComponentDestroy = true;
                IsTestFinished = true;
            }
            protected override void OnGameComponentDisable() => IsGameComponentEnable = false;
            protected override void OnGameComponentEnable() => IsGameComponentEnable = true;
        }
    }
}