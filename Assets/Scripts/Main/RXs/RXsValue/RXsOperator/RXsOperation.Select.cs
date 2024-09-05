using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IRXsOperatorToProperty<TResult> Select<TSource, TResult>(
            this IRXsProperty_Readonly<TSource> source,
            Func<TSource, TResult> selector,
            IRXsProperty<TResult> result = null)
        {
            result ??= new RXsProperty_SerializeField<TResult>();
            return RXsOperatorToProperty<TResult>.GetFromReusePool(
                source.AfterSet.Immediately().Subscribe(e => result.Value = selector(e.Current))
                , result);
        }
        public static IRXsOperatorToCollection<TResult> Select<TSource, TResult>(
            this IRXsCollection_Readonly<TSource> source,
            Func<TSource, TResult> selector,
            IRXsCollection<TResult> result = null)
        {
            result ??= new RXsCollection_SerializeField<TResult>();
            return RXsOperatorToCollection<TResult>.GetFromReusePool(
                RXsSubscription.FromList(
                    source.AfterAdd.Immediately().Subscribe(e => result.Add(selector(e.Item))),
                    source.AfterRemove.Subscribe(e => result.Remove(selector(e.Item))))
                , result);
        }
    }
}