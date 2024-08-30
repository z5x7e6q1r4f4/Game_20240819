using System.Collections;
using Main.RXs;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Main
{
    public class UnitTest_RXsTime
    {
        [Test]
        public void UnitTest_Timer([Values(1f, 5f, 10f)] float target, [Values(1f, 5f, 10f)] float delta)
        {
            RXsObserverList<ITimeData> timeNode = new();
            var timer = timeNode.GetTimer(target);
            Assert.AreEqual(0, timer.Time);
            Assert.AreEqual(target, timer.Target);
            //
            timer.OnUpdate.Subscribe(timer =>
            {
                Assert.AreEqual(delta, timer.Time);
                Assert.AreEqual(delta, timer.Delta);
            });
            bool hasArrive = false;
            timer.OnArrive.Subscribe(timer =>
            {
                hasArrive = true;
            });
            timeNode.OnNext(new TimeData(0, delta));
            //
            if (delta >= target) Assert.IsTrue(hasArrive);
            else Assert.IsFalse(hasArrive);
        }
        private class TimeData : ITimeData
        {
            public float Time { get; set; }
            public float Delta { get; set; }
            public TimeData(float time, float delta)
            {
                this.Time = time;
                this.Delta = delta;
            }
        }
    }
}
