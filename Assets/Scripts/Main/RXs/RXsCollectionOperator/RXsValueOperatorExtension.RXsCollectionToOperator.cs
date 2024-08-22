using Mono.Cecil;
using System;

namespace Main.RXs
{
    public static partial class RXsValueOperatorExtension
    {
        private abstract class RXsCollectionToOperator<TSource> : IDisposable
        {
            protected IRXsCollection_Readonly<TSource> Source { get; }
            private readonly DisposableList disposableList = new();
            public RXsCollectionToOperator(IRXsCollection_Readonly<TSource> source) => Source = source;
            public virtual void Subscribe()
            {
                if (disposableList.Count != 0) return;
                disposableList.Add(
                    Source.AfterAdd.Immediately().Subscribe(AfterAdd)
                    );
                disposableList.Add(
                    Source.AfterRemove.Subscribe(AfterRemove)
                    );
            }
            public virtual void Dispose()
            {
                var index = 0;
                foreach (var item in Source)
                {
                    using var eventArgs = EventArgs.GetFromReusePool(Source, index, item);
                    AfterRemove(eventArgs);
                    index++;
                }
                disposableList.Dispose();
            }
            protected abstract void AfterRemove(IRXsCollection_AfterRemove<TSource> e);
            protected abstract void AfterAdd(IRXsCollection_AfterAdd<TSource> e);
            private class EventArgs : Reuse.ObjectBase<EventArgs>, IRXsCollection_AfterRemove<TSource>, IDisposable
            {
                public IRXsCollection_Readonly<TSource> Collection { get; private set; }
                public int Index { get; private set; }
                public TSource Item { get; private set; }
                public static EventArgs GetFromReusePool(IRXsCollection_Readonly<TSource> collection, int index, TSource item)
                {
                    var eventArgs = GetFromReusePool();
                    eventArgs.Collection = collection;
                    eventArgs.Index = index;
                    eventArgs.Item = item;
                    return eventArgs;
                }
                void IDisposable.Dispose() => this.ReleaseToReusePool();
            }
        }
        private abstract class RXsCollectionToOperator : IDisposable
        {
            protected IRXsCollection_Readonly Source { get; }
            private readonly DisposableList disposableList = new();
            public RXsCollectionToOperator(IRXsCollection_Readonly source) => Source = source;
            public virtual void Subscribe()
            {
                if (disposableList.Count != 0) return;
                disposableList.Add(
                    Source.AfterAdd.Immediately().Subscribe(AfterAdd)
                    );
                disposableList.Add(
                    Source.AfterRemove.Subscribe(AfterRemove)
                    );
            }
            public virtual void Dispose()
            {
                var index = 0;
                foreach (var item in Source)
                {
                    using var eventArgs = EventArgs.GetFromReusePool(Source, index, item);
                    AfterRemove(eventArgs);
                    index++;
                }
                disposableList.Dispose();
            }
            protected abstract void AfterRemove(IRXsCollection_AfterRemove e);
            protected abstract void AfterAdd(IRXsCollection_AfterAdd e);
            private class EventArgs : Reuse.ObjectBase<EventArgs>, IRXsCollection_AfterRemove, IDisposable
            {
                public IRXsCollection_Readonly Collection { get; private set; }
                public int Index { get; private set; }
                public object Item { get; private set; }
                public static EventArgs GetFromReusePool(IRXsCollection_Readonly collection, int index, object item)
                {
                    var eventArgs = GetFromReusePool();
                    eventArgs.Collection = collection;
                    eventArgs.Index = index;
                    eventArgs.Item = item;
                    return eventArgs;
                }
                void IDisposable.Dispose() => this.ReleaseToReusePool();
            }
        }
    }
}