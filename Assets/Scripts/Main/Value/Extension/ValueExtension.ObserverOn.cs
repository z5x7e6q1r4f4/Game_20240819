using System;

namespace Main
{
    partial class ValueExtension
    {
        public static IOperatorToCollection<TResult> ObserverOn<TSource, TResult>(
            this IPropertyReadonly<TSource> source,
            Func<TSource, ICollectionReadonly<TResult>> func,
            ICollection<TResult> result = null)
        {
            result ??= new CollectionSerializeField<TResult>();
            IDisposable subscription = null;
            return OperatorToCollection<TResult>.GetFromReusePool(
                    source.AfterSet.Immediately().Subscribe(e =>
                    {
                        if (subscription != null) { subscription.Dispose(); subscription = null; }
                        if (e.Current != null) { var target = func(e.Current); subscription = target.ConnectTo(result); }
                    }, onDispose: ()=>subscription?.Dispose()),
                    result) ;
        }
        public static IOperatorToProperty<TResult> ObserverOn<TSource, TResult>(
            this IPropertyReadonly<TSource> source,
            Func<TSource, IPropertyReadonly<TResult>> func,
            IProperty<TResult> result = null)
        {
            result ??= new PropertySerializeField<TResult>();
            IDisposable subscription = null;
            return OperatorToProperty<TResult>.GetFromReusePool(
                  source.AfterSet.Immediately().Subscribe(e =>
                  {
                      if (subscription != null) { subscription.Dispose(); subscription = null; }
                      if (e.Current != null) { var target = func(e.Current); subscription = target.ConnectTo(result); }
                  }, onDispose: () => subscription?.Dispose()),
                  result);
        }
    }
}