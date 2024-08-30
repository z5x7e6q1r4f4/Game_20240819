using System;

namespace Main.RXs
{

    public abstract partial class RXsProperty<T>
    {
        private class EventHandler : RXsEventHandler<EventArgs>
        {

            public bool Invoke(IRXsProperty<T> property, T previous, T current, out T modified)
            {
                using var eventArgs = EventArgs.GetFromReusePool(property, previous, current);
                Invoke(eventArgs);
                modified = eventArgs.Modified;
                return eventArgs.IsEnable;
            }
            public EventHandler(Action<IObserver<EventArgs>> immediatelyAction = null) : base(immediatelyAction) { }
        }
        private void AfterSetImmediately(IObserver<EventArgs> observer)
        {
            using var eventArgs = EventArgs.GetFromReusePool(this, default, Value);
            observer.OnNext(eventArgs);
        }
    }
}