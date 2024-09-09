namespace Main
{
    public interface PropertyAfterSet
    {
        PropertyEventArgsType Type { get; }
        IPropertyReadonly Property { get; }
        bool IsSame { get; }
        object Previous { get; }
        object Current { get; }
    }
    public interface PropertyAfterSet<T> : PropertyAfterSet
    {
        IPropertyReadonly PropertyAfterSet.Property => Property;
        bool PropertyAfterSet.IsSame => Equals(Previous, Current);
        object PropertyAfterSet.Previous => Previous;
        object PropertyAfterSet.Current => Current;
        //
        new IPropertyReadonly<T> Property { get; }
        new T Previous { get; }
        new T Current { get; }
    }
}