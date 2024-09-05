using System;

namespace Main.RXs
{
    partial class Operation
    {
        public static IRXsOperatorToCollection<T> ConnectTo<T>(this IRXsCollection_Readonly<T> source, IRXsCollection<T> result)
            => RXsOperatorToCollection<T>.GetFromReusePool(
                 RXsSubscription.FromList(
                    source.AfterAdd.Immediately().Subscribe(e => result.Add(e.Item)),
                    source.AfterRemove.Subscribe(e => result.Remove(e.Item))),
                result);
        public static IRXsOperatorToCollection<T> ConnectFrom<T>(this IRXsCollection<T> result, IRXsCollection_Readonly<T> source)
            => ConnectTo(source, result);
        public static IRXsOperatorToProperty<T> ConnectTo<T>(this IRXsProperty_Readonly<T> source, IRXsProperty<T> result)
            => RXsOperatorToProperty<T>.GetFromReusePool(source.AfterSet.Immediately().Subscribe(e => result.Value = e.Current), result);
        public static IRXsOperatorToProperty<T> ConnectFrom<T>(this IRXsProperty<T> result, IRXsProperty_Readonly<T> source)
            => ConnectTo(source, result);
    }
}