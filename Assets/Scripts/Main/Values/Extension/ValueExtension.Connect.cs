namespace Main
{
    public static partial class ValueExtension
    {
        public static IOperatorToCollection<T> ConnectTo<T>(this ICollectionReadonly<T> source, ICollection<T> result)
            => OperatorToCollection<T>.GetFromReusePool(
                 Disposable.Create(
                    source.AfterAdd.Immediately().Subscribe(e => result.Add(e.Item)),
                    source.AfterRemove.Subscribe(e => result.Remove(e.Item))),
                result);
        public static IOperatorToCollection<T> ConnectFrom<T>(this ICollection<T> result, ICollectionReadonly<T> source)
            => ConnectTo(source, result);
        public static IOperatorToProperty<T> ConnectTo<T>(this IPropertyReadonly<T> source, IProperty<T> result)
            => OperatorToProperty<T>.GetFromReusePool(source.AfterSet.Immediately().Subscribe(e => result.Value = e.Current), result);
        public static IOperatorToProperty<T> ConnectFrom<T>(this IProperty<T> result, IPropertyReadonly<T> source)
            => ConnectTo(source, result);
    }
}