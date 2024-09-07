namespace Main.RXs
{
    partial class Operation
    {
        public static IOperatorToProperty<T> FirstOrDefault<T>(this IObservableCollection_Readonly<T> source, IObservableProperty<T> result = null)
            => source.ElementAt(0, result);
    }
}