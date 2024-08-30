using System;

namespace Main.RXs
{
    public abstract partial class RXsCollection<T>
    {
        private class EventArgs :
            Reuse.ObjectBase<EventArgs>,
            IRXsCollection_BeforeAdd<T>,
            IRXsCollection_AfterAdd<T>,
            IRXsCollection_BeforeRemove<T>,
            IRXsCollection_AfterRemove<T>,
            IDisposable
        {
            public IRXsCollection_Readonly<T> Collection { get; private set; }
            public bool IsEnable { get; set; }
            public int Index { get; private set; }
            public T Item { get; private set; }
            public T Modified { get; set; }
            public static EventArgs GetFromReusePool(IRXsCollection_Readonly<T> collection, int index, T item)
            {
                var eventArgs = GetFromReusePool();
                eventArgs.Collection = collection;
                eventArgs.IsEnable = true;
                eventArgs.Index = index;
                eventArgs.Item = item;
                eventArgs.Modified = item;
                return eventArgs;
            }
            void IDisposable.Dispose()=>this.ReleaseToReusePool();
        }
    }
}