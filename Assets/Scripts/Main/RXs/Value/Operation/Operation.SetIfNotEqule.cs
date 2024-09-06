namespace Main.RXs
{
    partial class Operation
    {
        public static void SetIfNotEqule<T>(this IObservableProperty<T> property, T value, bool beforeGet = true, bool beforeSet = true, bool afterSet = true)
        { if (!Equals(property.GetValue(beforeGet), value)) property.SetValue(value, beforeSet, afterSet); }
    }
}