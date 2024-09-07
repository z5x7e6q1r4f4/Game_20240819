using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IOperatorToCollection<T> Where<T>(this IObservableCollection_Readonly<T> source, Predicate<T> predicate, IObservableCollection<T> result = null)
        {
            result ??= new ObservableCollection_SerializeField<T>();
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