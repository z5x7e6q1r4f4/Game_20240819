using System;

namespace Main.RXs
{

    public abstract partial class RXsProperty<T>
    {
        private class EventHandler : RXsEventHandler<EventArgs>, IObservableImmediately<EventArgs>
        {
            private ISubject<EventArgs> immdiately;
            IObservable<EventArgs> IObservableImmediately<EventArgs>.Immediately() => immdiately;
            public bool Invoke(IRXsProperty<T> property, T previous, T current, out T modified)
            {
                using var eventArgs = EventArgs.GetFromReusePool(property, previous, current);
                Invoke(eventArgs);
                modified = eventArgs.Modified;
                return eventArgs.IsEnable;
            }
            public EventHandler(ISubject<EventArgs> immdiately = null)
            {
                if (immdiately == null) return;
                this.immdiately = immdiately;
                Subscribe(immdiately);
            }
        }
    }
}