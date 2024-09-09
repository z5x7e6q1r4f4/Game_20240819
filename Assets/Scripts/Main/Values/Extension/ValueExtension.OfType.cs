namespace Main
{
    partial class ValueExtension
    {
        public static IOperatorToProperty<T> OfType<T>(this IPropertyReadonly source, IProperty<T> result = null)
        {
            result ??= new PropertySerializeField<T>();
            return OperatorToProperty<T>.GetFromReusePool(
                    source.AfterSet.Immediately().Subscribe(e =>
                    {
                        if (e.Current is T typed) { result.Value = typed; return; }
                        if (Equals(e.Current, default(T))) result.Value = default;
                    }),
                    result);
        }
        public static IOperatorToCollection<T> OfType<T>(this ICollectionReadonly source, ICollection<T> result = null)
        {
            result ??= new CollectionSerializeField<T>();
            return OperatorToCollection<T>.GetFromReusePool(
                 Disposable.Create(
                    source.AfterAdd.Immediately().Subscribe(e =>
                    {
                        if (e.Item is T typed) { result.Add(typed); return; }
                        if (Equals(e.Item, default(T))) { result.Add(default); }
                    }),
                    source.AfterRemove.Subscribe(e =>
                    {
                        if (e.Item is T typed && result.Contains(typed)) { result.Remove(typed); return; }
                        if (Equals(e.Item, default(T)) && result.Contains(default)) { result.Remove(default); }
                    })),
                result);
        }
    }
}