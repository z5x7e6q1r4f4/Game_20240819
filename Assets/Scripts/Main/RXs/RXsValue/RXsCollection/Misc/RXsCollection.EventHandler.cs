using System;

namespace Main.RXs
{
    public abstract partial class RXsCollection<T>
    {
        private class EventHandler :
            RXsEventHandler<EventArgs>,
            IObservableImmediately<EventArgs>
        {
            private readonly ISubject<EventArgs> immediately;
            public bool Invoke(IRXsCollection_Readonly<T> collection, int index, T item, out T modified)
            {
                using var eventArgs = EventArgs.GetFromReusePool(collection, index, item);
                Invoke(eventArgs);
                modified = eventArgs.Modified;
                return eventArgs.IsEnable;
            }
            public EventHandler(Action<System.IObserver<EventArgs>> immediatelyAction = null) : base(immediatelyAction) { }
        }
        private void AfterAddImmediately(System.IObserver<EventArgs> observer)
        {
            var index = 0;
            foreach (var item in this)
            {
                using var eventArgs = EventArgs.GetFromReusePool(this, index, item);
                observer.OnNext(eventArgs);
                index++;
            }
        }
    }
}