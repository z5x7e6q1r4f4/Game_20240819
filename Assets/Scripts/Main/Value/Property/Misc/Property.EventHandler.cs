using System;

namespace Main
{

    public abstract partial class Property<T>
    {
        private class EventHandler : EventHandler<EventArgs>, IObservableImmediately<EventArgs>
        {
            public bool Invoke(IProperty<T> property, PropertyEventArgsType type, T previous, T current, out T modified)
            {
                using var eventArgs = EventArgs.GetFromReusePool(property, type, previous, current);
                Invoke(eventArgs);
                modified = eventArgs.Modified;
                return eventArgs.IsEnable;
            }
            public EventHandler(Action<IObserver<EventArgs>> immediatelyAction = null) : base(immediatelyAction) { }
        }
        private void AfterSetImmediately(IObserver<EventArgs> observer)
        {
            using var eventArgs = EventArgs.GetFromReusePool(this, PropertyEventArgsType.AfterSet, default, Value);
            observer.OnNext(eventArgs);
        }
    }
}