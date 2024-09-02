using System;

namespace Main.RXs
{
    partial class RXsOperation
    {
        public static IRXsOperatorToCollection<TResult> ObserverOn<TSource, TResult>(
            this IRXsProperty_Readonly<TSource> source,
            Func<TSource, IRXsCollection_Readonly<TResult>> func,
            IRXsCollection<TResult> result = null)
        {
            result ??= new RXsCollection_SerializeField<TResult>();
            IRXsSubscription subscription = null;
            return RXsOperatorToCollection<TResult>.GetFromReusePool(
                RXsSubscription.FromList(
                    source.AfterSet.Immediately().Subscribe(e =>
                    {
                        if (subscription != null) { subscription.Dispose(); subscription = null; }
                        if (e.Current != null) { var target = func(e.Current); subscription = target.ConnectTo(result); }
                    }),
                    RXsSubscription.FromAction(onRelease: () => subscription?.Dispose())),
                    result);
        }
        public static IRXsOperatorToProperty<TResult> ObserverOn<TSource, TResult>(
            this IRXsProperty_Readonly<TSource> source,
            Func<TSource, IRXsProperty_Readonly<TResult>> func,
            IRXsProperty<TResult> result = null)
        {
            result ??= new RXsProperty_SerializeField<TResult>();
            IRXsSubscription subscription = null;
            return RXsOperatorToProperty<TResult>.GetFromReusePool(
                  RXsSubscription.FromList(
                  source.AfterSet.Immediately().Subscribe(e =>
                  {
                      if (subscription != null) { subscription.Dispose(); subscription = null; }
                      if (e.Current != null) { var target = func(e.Current); subscription = target.ConnectTo(result); }
                  }),
                  RXsSubscription.FromAction(onRelease: () => subscription?.Dispose())),
                  result);
        }
    }
}