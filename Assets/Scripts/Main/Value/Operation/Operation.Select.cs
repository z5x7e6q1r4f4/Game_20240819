using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IOperatorToProperty<TResult> Select<TSource, TResult>(
            this IObservableProperty_Readonly<TSource> source,
            Func<TSource, TResult> selector,
            IObservableProperty<TResult> result = null)
        {
            result ??= new ObservableProperty_SerializeField<TResult>();
            return OperatorToProperty<TResult>.GetFromReusePool(
                source.AfterSet.Immediately().Subscribe(e => result.Value = selector(e.Current))
                , result);
        }
        public static IOperatorToCollection<TResult> Select<TSource, TResult>(
            this IObservableCollection_Readonly<TSource> source,
            Func<TSource, TResult> selector,
            IObservableCollection<TResult> result = null)
        {
            result ??= new ObservableCollection_SerializeField<TResult>();
            return OperatorToCollection<TResult>.GetFromReusePool(
                Disposable.Create(
                    source.AfterAdd.Immediately().Subscribe(e => result.Add(selector(e.Item))),
                    source.AfterRemove.Subscribe(e => result.Remove(selector(e.Item))))
                , result);
        }
    }
}