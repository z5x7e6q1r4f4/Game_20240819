namespace Main.RXs
{
    partial class Operation
    {
        public static IRXsOperatorToProperty<T> FirstOrDefault<T>(this IRXsCollection_Readonly<T> source, IRXsProperty<T> result = null)
            => source.ElementAt(0, result);
    }
}