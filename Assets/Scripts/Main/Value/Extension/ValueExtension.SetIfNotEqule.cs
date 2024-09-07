namespace Main
{
    partial class ValueExtension
    {
        public static void SetIfNotEqule<T>(this IProperty<T> property, T value, bool beforeGet = true, bool beforeSet = true, bool afterSet = true)
        { if (!Equals(property.GetValue(beforeGet), value)) property.SetValue(value, beforeSet, afterSet); }
    }
}