using System.Collections;
using Main.RXs;
using NUnit.Framework;
using NSubstitute;
using UnityEngine.TestTools;

namespace Main
{
    public class UnitTest_RXsTime
    {
        [Test]
        public void UnitTest_Timer([Values(1f, 5f, 10f)] float target, [Values(1f, 5f, 10f)] float delta)
        {
            var timeData = Substitute.For<ITimeData>();
            timeData.Delta.Returns(delta);
            EventHandler<ITimeData> timeNode = new();
            using var timer = timeNode.GetTimer(target);
            Assert.AreEqual(0, timer.Time.Value);
            Assert.AreEqual(target, timer.Target.Value);
            //
            using var _1 = ((ITimeUpdator)timer).OnUpdate.Subscribe(timer =>
                {
                    Assert.AreEqual(delta, timer.Time.Value);
                    Assert.AreEqual(delta, timer.Delta.Value);
                });
            bool hasArrive = false;
            using var _2 = timer.OnArrive.Subscribe(timer =>
                  {
                      hasArrive = true;
                  });
            timeNode.Invoke(timeData);
            //
            if (delta >= target) Assert.IsTrue(hasArrive);
            else Assert.IsFalse(hasArrive);
        }
    }
}
