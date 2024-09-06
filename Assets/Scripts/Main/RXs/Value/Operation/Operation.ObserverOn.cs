using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IOperatorToCollection<TResult> ObserverOn<TSource, TResult>(
            this IObservableProperty_Readonly<TSource> source,
            Func<TSource, IObservableCollection_Readonly<TResult>> func,
            IObservableCollection<TResult> result = null)
        {
            result ??= new ObservableCollection_SerializeField<TResult>();
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
            this IObservableProperty_Readonly<TSource> source,
            Func<TSource, IObservableProperty_Readonly<TResult>> func,
            IObservableProperty<TResult> result = null)
        {
            result ??= new ObservableProperty_SerializeField<TResult>();
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