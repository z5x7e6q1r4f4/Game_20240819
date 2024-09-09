namespace Main
{
    public interface PropertyBeforeSet
    {
        PropertyEventArgsType Type { get; }
        IPropertyReadonly Property { get; }
        bool IsEnable { get; set; }
        bool IsSame { get; }
        object Previous { get; }
        object Current { get; }
        object Modified { get; set; }
    }
    public interface PropertyBeforeSet<T> : PropertyBeforeSet
    {
        IPropertyReadonly PropertyBeforeSet.Property => Property;
        bool PropertyBeforeSet.IsSame => Equals(Previous, Current);
        object PropertyBeforeSet.Previous => Previous;
        object PropertyBeforeSet.Current => Current;
        object PropertyBeforeSet.Modified { get => Modified; set => Modified = (T)value; }
        //
        new IPropertyReadonly<T> Property { get; }
        new T Previous { get; }
        new T Current { get; }
        new T Modified { get; set; }
    }
}