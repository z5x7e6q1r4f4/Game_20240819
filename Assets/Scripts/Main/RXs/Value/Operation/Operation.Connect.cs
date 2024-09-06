using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IOperatorToCollection<T> ConnectTo<T>(this IObservableCollection_Readonly<T> source, IObservableCollection<T> result)
            => OperatorToCollection<T>.GetFromReusePool(
                 Disposable.Create(
                    source.AfterAdd.Immediately().Subscribe(e => result.Add(e.Item)),
                    source.AfterRemove.Subscribe(e => result.Remove(e.Item))),
                result);
        public static IOperatorToCollection<T> ConnectFrom<T>(this IObservableCollection<T> result, IObservableCollection_Readonly<T> source)
            => ConnectTo(source, result);
        public static IOperatorToProperty<T> ConnectTo<T>(this IObservableProperty_Readonly<T> source, IObservableProperty<T> result)
            => OperatorToProperty<T>.GetFromReusePool(source.AfterSet.Immediately().Subscribe(e => result.Value = e.Current), result);
        public static IOperatorToProperty<T> ConnectFrom<T>(this IObservableProperty<T> result, IObservableProperty_Readonly<T> source)
            => ConnectTo(source, result);
    }
}