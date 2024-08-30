using System;

namespace Main.RXs
{
    partial class RXsValueOperatorExtension
    {
        private class LinkItemOperator<TCollectionSelf, TItem> : RXsCollectionToOperator<TItem>
        {
            TCollectionSelf Self { get; }
            Func<TItem, IRXsProperty<TCollectionSelf>> Func { get; }
            public LinkItemOperator(IRXsCollection_Readonly<TItem> source, TCollectionSelf self, Func<TItem, IRXsProperty<TCollectionSelf>> func) : base(source)
            {
                Self = self;
                Func = func;
                Subscribe();
            }
            protected override void AfterAdd(IRXsCollection_AfterAdd<TItem> e)
            {
                var value = Func(e.Item);
                if (Equals(value.Value, Self)) return;
                value.Value = Self;
            }
            protected override void AfterRemove(IRXsCollection_AfterRemove<TItem> e)
            {
                var value = Func(e.Item);
                if (!Equals(value.Value, Self)) return;
                value.Value = default;
            }
        }
        private class LinkCollectionOperator<TItemSelf, TCollection> : RXsPropertyToOperator<TCollection>
        {
            TItemSelf Self { get; }
            Func<TCollection, IRXsCollection<TItemSelf>> Func { get; }
            public LinkCollectionOperator(IRXsProperty_Readonly<TCollection> source, TItemSelf self, Func<TCollection, IRXsCollection<TItemSelf>> func) : base(source)
            {
                Self = self;
                Func = func;
                Subscribe();
            }
            protected override void AfterSet(IRXsProperty_AfterSet<TCollection> e)
            {
                if (e.Previous != null)
                {
                    var collection = Func(e.Previous);
                    if (collection.Contains(Self)) collection.Remove(Self);
                }
                if (e.Current != null)
                {
                    var collection = Func(e.Current);
                    if (!collection.Contains(Self)) collection.Add(Self);
                }
            }
        }
        public static IDisposable LinkItem<TCollectionSelf, TItem>(this IRXsCollection_Readonly<TItem> source, TCollectionSelf self, Func<TItem, IRXsProperty<TCollectionSelf>> func)
            => new LinkItemOperator<TCollectionSelf, TItem>(source, self, func);
        public static IDisposable LinkCollection<TItemSelf, TCollection>(this IRXsProperty_Readonly<TCollection> source, TItemSelf self, Func<TCollection, IRXsCollection<TItemSelf>> func)
            => new LinkCollectionOperator<TItemSelf, TCollection>(source, self, func);
    }
}