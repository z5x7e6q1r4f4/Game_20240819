using System;
namespace Main.RXs
{
    public abstract partial class RXsCollection<T>
    {
        private class AfterAddImmediately : EventHandler
        {
            private readonly IRXsCollection_Readonly<T> collection;
            public override IDisposable Subscribe(System.IObserver<EventArgs> observer)
            {
                var index = 0;
                foreach (var item in collection)
                {
                    using var eventArgs = EventArgs.GetFromReusePool(collection, index, item);
                    observer.OnNext(eventArgs);
                    index++;
                }
                return base.Subscribe(observer);
            }
            public AfterAddImmediately(IRXsCollection_Readonly<T> collection) => this.collection = collection;
        }
    }
}