using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IRXsDisposable LinkItem<TCollectionSelf, TItem>(this IRXsCollection_Readonly<TItem> source, TCollectionSelf self, Func<TItem, IRXsProperty<TCollectionSelf>> func)
            => RXsSubscription.FromList(
                    source.AfterAdd.Immediately().Subscribe(e =>
                    {
                        if (e.Item == null) return;
                        var target = func(e.Item);
                        if (!self.Equals(target.Value)) target.Value = self;
                    }),
                    source.AfterRemove.Subscribe(e =>
                    {
                        if (e.Item == null) return;
                        var target = func(e.Item);
                        if (self.Equals(target.Value)) target.Value = default;
                    })
                );
        public static IRXsDisposable LinkCollection<TItemSelf, TCollection>(this IRXsProperty_Readonly<TCollection> source, TItemSelf self, Func<TCollection, IRXsCollection<TItemSelf>> func)
            => source.AfterSet.Immediately().Subscribe(e =>
            {
                if (e.Previous != null)
                {
                    var target = func(e.Previous);
                    if (target.Contains(self)) target.Remove(self);
                }
                if (e.Current != null)
                {
                    var target = func(e.Current);
                    if (!target.Contains(self)) target.Add(self);
                }
            });
    }
}