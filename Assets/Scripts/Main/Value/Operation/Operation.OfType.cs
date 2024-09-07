namespace Main.RXs
{
    partial class Operation
    {
        public static IOperatorToProperty<T> OfType<T>(this IObservableProperty_Readonly source, IObservableProperty<T> result = null)
        {
            result ??= new ObservableProperty_SerializeField<T>();
            return OperatorToProperty<T>.GetFromReusePool(
                    source.AfterSet.Immediately().Subscribe(e =>
                    {
                        if (e.Current is T typed) { result.Value = typed; return; }
                        if (Equals(e.Current, default(T))) result.Value = default;
                    }),
                    result);
        }
        public static IOperatorToCollection<T> OfType<T>(this IObservableCollection_Readonly source, IObservableCollection<T> result = null)
        {
            result ??= new ObservableCollection_SerializeField<T>();
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