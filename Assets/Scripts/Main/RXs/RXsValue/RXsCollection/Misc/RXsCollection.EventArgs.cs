using Main.RXs.RXsCollections;
using System;
using static Codice.CM.Common.CmCallContext;

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
            public RXsCollectionEventArgsType Type { get; private set; }
            public bool IsEnable { get; set; }
            public int Index { get; private set; }
            public T Item { get; private set; }
            public T Modified { get; set; }
            public static EventArgs GetFromReusePool(IRXsCollection_Readonly<T> collection, RXsCollectionEventArgsType type, int index, T item)
            {
                var eventArgs = GetFromReusePool();
                eventArgs.Collection = collection;
                eventArgs.Type = type;
                eventArgs.IsEnable = true;
                eventArgs.Index = index;
                eventArgs.Item = item;
                eventArgs.Modified = item;
                return eventArgs;
            }
            void IDisposable.Dispose() => this.ReleaseToReusePool();
            public override string ToString()
            {
                string type = $"<color=yellow>{Type}</color> ";
                string isEnable = $"IsEnable:{(IsEnable ? "<color=green>Enable</color>" : "<color=red>Disable</color>")},";
                string index = $"Index:<color=green>{Index}</color>,";
                string item = $"Current:<color=green>{Item}</color>";
                string modified = !Equals(Modified, Item) ? $",Modified:<color=yellow>{Modified}</color>" : string.Empty;
                return Type switch
                {
                    RXsCollectionEventArgsType.BeforeAdd => $"{type}{isEnable}{index}{item}{modified}",
                    RXsCollectionEventArgsType.AfterAdd => $"{type}{index}{item}",
                    RXsCollectionEventArgsType.BeforeRemove => $"{type}{isEnable}{index}{item}",
                    RXsCollectionEventArgsType.AfterRemove => $"{type}{index}{item}",
                    _ => throw new Exception(),
                };
            }
        }
    }
}