using System;

namespace Main
{
    partial class ValueExtension
    {
        public static IOperatorToProperty<TResult> Select<TSource, TResult>(
            this IPropertyReadonly<TSource> source,
            Func<TSource, TResult> selector,
            IProperty<TResult> result = null)
        {
            result ??= new PropertySerializeField<TResult>();
            return OperatorToProperty<TResult>.GetFromReusePool(
                source.AfterSet.Immediately().Subscribe(e => result.Value = selector(e.Current))
                , result);
        }
        public static IOperatorToCollection<TResult> Select<TSource, TResult>(
            this ICollectionReadonly<TSource> source,
            Func<TSource, TResult> selector,
            ICollection<TResult> result = null)
        {
            result ??= new CollectionSerializeField<TResult>();
            return OperatorToCollection<TResult>.GetFromReusePool(
                Disposable.Create(
                    source.AfterAdd.Immediately().Subscribe(e => result.Add(selector(e.Item))),
                    source.AfterRemove.Subscribe(e => result.Remove(selector(e.Item))))
                , result);
        }
    }
}