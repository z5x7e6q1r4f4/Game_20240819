namespace Main.RXs
{
    partial class RXsOperation
    {
        public static IRXsOperatorToProperty<T> OfType<T>(this IRXsProperty_Readonly source, IRXsProperty<T> result = null)
        {
            result ??= new RXsProperty_SerializeField<T>();
            return RXsOperatorToProperty<T>.GetFromReusePool(
                    source.AfterSet.Immediately().Subscribe(e =>
                    {
                        if (e.Current is T typed) { result.Value = typed; return; }
                        if (Equals(e.Current, default(T))) result.Value = default;
                    }),
                    result);
        }
        public static IRXsOperatorToCollection<T> OfType<T>(this IRXsCollection_Readonly source, IRXsCollection<T> result = null)
        {
            result ??= new RXsCollection_SerializeField<T>();
            return RXsOperatorToCollection<T>.GetFromReusePool(
                new RXsSubscriptionList(
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