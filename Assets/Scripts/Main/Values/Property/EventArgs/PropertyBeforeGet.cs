namespace Main
{
    public interface PropertyBeforeGet
    {
        PropertyEventArgsType Type { get; }
        IPropertyReadonly Property { get; }
        object Previous { get; }
        object Modified { get; set; }
    }
    public interface PropertyBeforeGet<T> : PropertyBeforeGet
    {
        IPropertyReadonly PropertyBeforeGet.Property => Property;
        object PropertyBeforeGet.Modified { get => Modified; set => Modified = (T)value; }
        object PropertyBeforeGet.Previous => Previous;
        new IPropertyReadonly<T> Property { get; }
        new T Previous { get; }
        new T Modified { get; set; }
    }
}