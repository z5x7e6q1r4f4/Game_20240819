using Main.RXs.ObservableCollections;
using System;

namespace Main.RXs
{
    public abstract partial class ObservableCollectionBase<T>
    {
        private class EventHandler :
            EventHandler<EventArgs>,
            IObservableImmediately<EventArgs>
        {
            public bool Invoke(IObservableCollection_Readonly<T> collection, ObservableCollectionEventArgsType type, int index, T item, out T modified)
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
                using var eventArgs = EventArgs.GetFromReusePool(this, ObservableCollectionEventArgsType.AfterAdd, index, item);
                observer.OnNext(eventArgs);
                index++;
            }
        }
    }
}