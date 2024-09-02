using System;

namespace Main.RXs
{
    partial class RXsOperation
    {
        public static IRXsOperatorToCollection<T> Where<T>(this IRXsCollection_Readonly<T> source, Predicate<T> predicate, IRXsCollection<T> result = null)
        {
            result ??= new RXsCollection_SerializeField<T>();
            return RXsOperatorToCollection<T>.GetFromReusePool(
                new RXsSubscriptionList(
                    source.AfterAdd.Immediately().Subscribe(e =>
                    {
                        if (predicate(e.Item)) result.Add(e.Item);
                    }),
                    source.AfterRemove.Subscribe(e =>
                    {
                        if (result.Contains(e.Item)) result.Remove(e.Item);
                    })),
                result);
        }
    }
}