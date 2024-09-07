using System;

namespace Main
{
    partial class ValueExtension
    {
        public static IOperatorToCollection<T> Where<T>(this ICollectionReadonly<T> source, Predicate<T> predicate, ICollection<T> result = null)
        {
            result ??= new CollectionSerializeField<T>();
            return OperatorToCollection<T>.GetFromReusePool(
                 Disposable.Create(
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