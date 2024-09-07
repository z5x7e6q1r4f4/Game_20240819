namespace Main
{
    public interface IPropertyReadonly
    {
        object Value { get; }
        IObservableImmediately<PropertyAfterSet> AfterSet { get; }
        object GetValue(bool beforeGet = true);
    }
    public interface IPropertyReadonly<T> : IPropertyReadonly
    {
        object IPropertyReadonly.Value => Value;
        IObservableImmediately<PropertyAfterSet> IPropertyReadonly.AfterSet => AfterSet;
        object IPropertyReadonly.GetValue(bool beforeGet) => GetValue(beforeGet);
        //
        new T Value { get; }
        new IObservableImmediately<PropertyAfterSet<T>> AfterSet { get; }
        new T GetValue(bool beforeGet = true);
    }
}