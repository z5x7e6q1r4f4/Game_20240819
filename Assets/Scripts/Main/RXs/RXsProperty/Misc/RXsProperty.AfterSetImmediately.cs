using System;

namespace Main.RXs
{

    public abstract partial class RXsProperty<T>
    {
        private class AfterSetImmediately : EventHandler
        {
            private T previous;
            private readonly IRXsProperty_Readonly<T> property;
            protected override void OnNext(EventArgs value)
            {
                previous = value.Previous;
                base.OnNext(value);
            }
            public override IDisposable Subscribe(System.IObserver<EventArgs> observer)
            {
                using var eventArgs = EventArgs.GetFromReusePool(property, previous, property.Value);
                observer.OnNext(eventArgs);
                return base.Subscribe(observer);
            }
            public AfterSetImmediately(IRXsProperty_Readonly<T> property) => this.property = property;
        }
    }
}