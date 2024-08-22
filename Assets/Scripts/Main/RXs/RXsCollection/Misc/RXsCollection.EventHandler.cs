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
            IObservable<EventArgs> IObservableImmediately<EventArgs>.Immediately() => immediately;
            public EventHandler(ISubject<EventArgs> immediately = null)
            {
                if (immediately == null) return;
                this.immediately = immediately;
                Subscribe(immediately);
            }
        }
    }
}