using NUnit.Framework;
using System;

namespace Main
{
    public class UnitTest_EventHandler
    {
        [Test]
        public void Subscribe()
        {
            var input = "input";
            var eventHandler = new EventHandler<string>();
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
            string result = null;
            var eventHandler = new EventHandler<string>();
            eventHandler.Subscribe(value => result = value);
            eventHandler.Invoke("1");
            Assert.AreEqual("1", result);
            eventHandler.Clear();
            eventHandler.Invoke("1");
            Assert.AreEqual("1", result);
        }
    }
}