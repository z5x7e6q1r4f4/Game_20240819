namespace Main.RXs
{
    partial class Operation
    {
        public static IRXsOperatorToProperty<T> ElementAt<T>(this IRXsCollection_Readonly<T> source, int index, IRXsProperty<T> result = null)
        {
            result ??= new RXsProperty_SerializeField<T>();
            return RXsOperatorToProperty<T>.GetFromReusePool(
                   RXsSubscription.FromList(
                      source.AfterAdd.Immediately().Subscribe(e => { if (e.Index == index) result.Value = e.Item; }),
                      source.AfterRemove.Subscribe(e => { if (e.Index == index) result.Value = source.GetAt(index); })),
                  result);
        }
    }
}