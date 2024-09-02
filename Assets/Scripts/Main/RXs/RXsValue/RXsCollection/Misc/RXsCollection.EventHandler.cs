using Main.RXs.RXsCollections;
using System;

namespace Main.RXs
{
    public abstract partial class RXsCollection<T>
    {
        private class EventHandler :
            RXsEventHandler<EventArgs>,
            IRXsObservableImmediately<EventArgs>
        {
            public bool Invoke(IRXsCollection_Readonly<T> collection, RXsCollectionEventArgsType type, int index, T item, out T modified)
            {
                using var eventArgs = EventArgs.GetFromReusePool(collection, type, index, item);
                Invoke(eventArgs);
                modified = eventArgs.Modified;
                return eventArgs.IsEnable;
            }
            public EventHandler(Action<IObserver<EventArgs>> immediatelyAction = null) : base(immediatelyAction) { }
        }
        private void AfterAddImmediately(IObserver<EventArgs> observer)
        {
            var index = 0;
            foreach (var item in this)
            {
                using var eventArgs = EventArgs.GetFromReusePool(this, RXsCollectionEventArgsType.AfterAdd, index, item);
                observer.OnNext(eventArgs);
                index++;
            }
        }
    }
}