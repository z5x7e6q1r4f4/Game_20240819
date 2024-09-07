using Main.RXs.ObservableCollections;
using System;

namespace Main.RXs
{
    public abstract partial class ObservableCollectionBase<T>
    {
        private class EventArgs :
            Reuse.ObjectBase<EventArgs>,
            IObservableCollection_BeforeAdd<T>,
            IObservableCollection_AfterAdd<T>,
            IObservableCollection_BeforeRemove<T>,
            IObservableCollection_AfterRemove<T>,
            IDisposable
        {
            public IObservableCollection_Readonly<T> Collection { get; private set; }
            public ObservableCollectionEventArgsType Type { get; private set; }
            public bool IsEnable { get; set; }
            public int Index { get; private set; }
            public T Item { get; private set; }
            public T Modified { get; set; }
            public static EventArgs GetFromReusePool(IObservableCollection_Readonly<T> collection, ObservableCollectionEventArgsType type, int index, T item)
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
                    ObservableCollectionEventArgsType.BeforeAdd => $"{type}{isEnable}{index}{item}{modified}",
                    ObservableCollectionEventArgsType.AfterAdd => $"{type}{index}{item}",
                    ObservableCollectionEventArgsType.BeforeRemove => $"{type}{isEnable}{index}{item}",
                    ObservableCollectionEventArgsType.AfterRemove => $"{type}{index}{item}",
                    _ => throw new Exception(),
                };
            }
        }
    }
}