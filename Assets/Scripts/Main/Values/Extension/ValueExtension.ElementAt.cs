namespace Main
{
    partial class ValueExtension
    {
        public static IOperatorToProperty<T> ElementAt<T>(this ICollectionReadonly<T> source, int index, IProperty<T> result = null)
        {
            result ??= new PropertySerializeField<T>();
            return OperatorToProperty<T>.GetFromReusePool(
                   Disposable.Create(
                      source.AfterAdd.Immediately().Subscribe(e => { if (e.Index == index) result.Value = e.Item; }),
                      source.AfterRemove.Subscribe(e => { if (e.Index == index) result.Value = source.GetAt(index); })),
                  result);
        }
    }
}