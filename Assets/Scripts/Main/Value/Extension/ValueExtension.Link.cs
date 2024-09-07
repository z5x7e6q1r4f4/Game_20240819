using System;

namespace Main
{
    partial class ValueExtension
    {
        public static IDisposable LinkItem<TCollectionSelf, TItem>(this ICollectionReadonly<TItem> source, TCollectionSelf self, Func<TItem, IProperty<TCollectionSelf>> func)
            => Disposable.Create(
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
        public static IDisposable LinkCollection<TItemSelf, TCollection>(this IPropertyReadonly<TCollection> source, TItemSelf self, Func<TCollection, ICollection<TItemSelf>> func)
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