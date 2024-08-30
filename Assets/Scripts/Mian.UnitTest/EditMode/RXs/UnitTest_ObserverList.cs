using NUnit.Framework;
using UnityEditor.PackageManager;

namespace Main.RXs
{
    public class UnitTest_ObserverList
    {
        [Test]
        public void Subscribe()
        {
            int result = default;
            bool onCompleted = false;
            bool onError = false;
            RXsObserverList<int> observerList = new();
            observerList.Subscribe(e => result = e, () => onCompleted = true, e => onError = true);
            //
            observerList.OnNext(1);
            Assert.AreEqual(1, result);
            result = default;
            observerList.OnNext((object)1);
            Assert.AreEqual(1, result);
            //
            observerList.OnCompleted();
            Assert.AreEqual(true, onCompleted);
            //
            observerList.OnError(null);
            Assert.AreEqual(true, onError);
        }
        [Test]
        public void Dispose()
        {
            Reuse.Clear();
            RXsObserverList<int> observerList = new();
            var observer = observerList.Subscribe(() => { });
            Reuse.Log();
            observerList.Dispose();
            Reuse.Log();
        }
    }
}