using Main.RXs.RXsProperties;
using System;

namespace Main.RXs
{

    public abstract partial class ObservablePropertyBase<T>
    {
        private class EventHandler : ObservableEventHandler<EventArgs>, IObservableImmediately<EventArgs>
        {
            public bool Invoke(IObservableProperty<T> property, ObservablePropertyEventArgsType type, T previous, T current, out T modified)
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
            using var eventArgs = EventArgs.GetFromReusePool(this, ObservablePropertyEventArgsType.AfterSet, default, Value);
            observer.OnNext(eventArgs);
        }
    }
}