namespace Main.RXs
{
    partial class Operation
    {
        public static IOperatorToProperty<T> ElementAt<T>(this IObservableCollection_Readonly<T> source, int index, IObservableProperty<T> result = null)
        {
            result ??= new ObservableProperty_SerializeField<T>();
            return OperatorToProperty<T>.GetFromReusePool(
                   Disposable.Create(
                      source.AfterAdd.Immediately().Subscribe(e => { if (e.Index == index) result.Value = e.Item; }),
                      source.AfterRemove.Subscribe(e => { if (e.Index == index) result.Value = source.GetAt(index); })),
                  result);
        }
    }
}