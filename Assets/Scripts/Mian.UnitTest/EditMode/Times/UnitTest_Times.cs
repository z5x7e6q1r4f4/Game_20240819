using System.Collections;
using NUnit.Framework;
using NSubstitute;
using UnityEngine.TestTools;
using UnityEngine;

namespace Main
{
    public class UnitTest_Times
    {
        [Test]
        public void GetTimer([Values(1f, 5f, 10f)] float target, [Values(1f, 5f, 10f)] float delta)
        {
            var timeData = Substitute.For<ITimeData>();
            timeData.Delta.Returns(delta);
            EventHandler<ITimeData> timeNode = new();
            using var timer = timeNode.GetTimer(target);
            Assert.AreEqual(0, timer.Time.Value);
            Assert.AreEqual(target, timer.Target.Value);
            //
            using var _1 = timer.OnUpdate.Subscribe(timer =>
                {
                    Assert.AreEqual(delta, timer.Time.Value);
                    Assert.AreEqual(delta, timer.Delta.Value);
                });
            bool hasArrive = false;
            using var _2 = timer.OnArrive.Subscribe(timer => hasArrive = true);
            timeNode.Invoke(timeData);
            //
            if (delta >= target) Assert.IsTrue(hasArrive);
            else Assert.IsFalse(hasArrive);
        }
        [Test]
        public void GetTimerAutoDispose([Values(1f, 5f, 10f)] float target, [Values(1f, 5f, 10f)] float delta)
        {
            bool hasArrive = false;
            var timeData = Substitute.For<ITimeData>();
            timeData.Delta.Returns(delta);
            EventHandler<ITimeData> timeNode = new();
            using var timer = timeNode.GetTimer(target, () => hasArrive = true);
            timeNode.Invoke(timeData);
            if (delta >= target)
            {
                Assert.IsTrue(hasArrive);
                Assert.IsTrue(Reuse.IsInactive(timer));
            }
            else Assert.IsFalse(hasArrive);
        }
        [Test]
        public void Every([Values(1f, 5f, 10f)] float delta, [Values(1f, 5f, 10f)] float every)
        {
            int count = 0;
            var timeData = Substitute.For<ITimeData>();
            timeData.Delta.Returns(delta);
            EventHandler<ITimeData> timeNode = new();
            using var _ = timeNode.Every(every).Subscribe(() => count++);
            timeNode.Invoke(timeData);
            Assert.AreEqual(Mathf.FloorToInt(delta / every), count);
        }
    }
}
