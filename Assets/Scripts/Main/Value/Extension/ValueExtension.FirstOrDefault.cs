namespace Main
{
    partial class ValueExtension
    {
        public static IOperatorToProperty<T> FirstOrDefault<T>(this ICollectionReadonly<T> source, IProperty<T> result = null)
            => source.ElementAt(0, result);
    }
}