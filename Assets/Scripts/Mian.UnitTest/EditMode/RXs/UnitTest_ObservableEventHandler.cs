using NUnit.Framework;
using System;

namespace Main.RXs
{
    public class UnitTest_ObservableEventHandler
    {
        [Test]
        public void Test_Subscribe()
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
        public void Test_Clear()
        {
            Reuse.Clear();
            var eventHandler = new EventHandler<string>();
            eventHandler.Subscribe(() => { });
            Reuse.Log();
            eventHandler.Clear();
            Reuse.Log();
        }
    }
}