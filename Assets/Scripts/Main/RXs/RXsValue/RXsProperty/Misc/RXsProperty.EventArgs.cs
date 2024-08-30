using System;

namespace Main.RXs
{

    public abstract partial class RXsProperty<T>
    {
        private class EventArgs :
            Reuse.ObjectBase<EventArgs>,
            IRXsProperty_BeforeSet<T>,
            IRXsProperty_AfterSet<T>,
            IDisposable
        {
            public IRXsProperty_Readonly<T> Property { get; private set; }
            public bool IsEnable { get; set; }
            public T Previous { get; private set; }
            public T Current { get; private set; }
            public T Modified { get; set; }
            public static EventArgs GetFromReusePool(IRXsProperty_Readonly<T> property, T previous, T current)
            {
                var eventArgs = GetFromReusePool();
                eventArgs.Property = property;
                eventArgs.IsEnable = true;
                eventArgs.Previous = previous;
                eventArgs.Current = current;
                eventArgs.Modified = current;
                return eventArgs;
            }
            public void Dispose() => this.ReleaseToReusePool();
        }
    }
}