namespace Main.RXs
{
    public interface IRXsProperty_AfterSet
    {
        IRXsProperty_Readonly Property { get; }
        object Previous { get; }
        object Current { get; }
    }
    public interface IRXsProperty_AfterSet<T> : IRXsProperty_AfterSet
    {
        IRXsProperty_Readonly IRXsProperty_AfterSet.Property => Property;
        object IRXsProperty_AfterSet.Previous => Previous;
        object IRXsProperty_AfterSet.Current => Current;
        //
        new IRXsProperty_Readonly<T> Property { get; }
        new T Previous { get; }
        new T Current { get; }
    }
}