using NUnit.Framework;
using System;

namespace Main.RXs
{
    public class UnitTest_RXsEventHandler
    {
        [Test]
        public void Subscribe()
        {
            var input = "input";
            var eventHandler = new RXsEventHandler<string>();
            int invokeCount = 0;
            Action<string> action = e =>
            {
                invokeCount++;
                Assert.AreEqual(input, e);
            };
            using var _ = eventHandler.Subscribe(action);
            eventHandler.Invoke(input);
            Assert.AreEqual(1, invokeCount);
            eventHandler.Invoke(input);
            Assert.AreEqual(2, invokeCount);
            eventHandler.Invoke(input);
            Assert.AreEqual(3, invokeCount);
        }
        [Test]
        public void Clear()
        {
            Reuse.Clear();
            var eventHandler = new RXsEventHandler<string>();
            eventHandler.Subscribe(() => { });
            Reuse.Log();
            eventHandler.Clear();
            Reuse.Log();
        }
    }
}