using System;

namespace Main
{
    public abstract partial class Collection<T>
    {
        private class EventArgs :
            Reuse.ObjectBase<EventArgs>,
            CollectionBeforeAdd<T>,
            CollectionAfterAdd<T>,
            CollectionBeforeRemove<T>,
            CollectionAfterRemove<T>,
            IDisposable
        {
            public ICollectionReadonly<T> Collection { get; private set; }
            public CollectionEventArgsType Type { get; private set; }
            public bool IsEnable { get; set; }
            public int Index { get; private set; }
            public T Item { get; private set; }
            public T Modified { get; set; }
            public static EventArgs GetFromReusePool(ICollectionReadonly<T> collection, CollectionEventArgsType type, int index, T item)
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
                    CollectionEventArgsType.BeforeAdd => $"{type}{isEnable}{index}{item}{modified}",
                    CollectionEventArgsType.AfterAdd => $"{type}{index}{item}",
                    CollectionEventArgsType.BeforeRemove => $"{type}{isEnable}{index}{item}",
                    CollectionEventArgsType.AfterRemove => $"{type}{index}{item}",
                    _ => throw new Exception(),
                };
            }
        }
    }
}