namespace Main.RXs
{
    public interface IRXsProperty_Readonly
    {
        object Value { get; }
        IRXsObservableImmediately<IRXsProperty_AfterSet> AfterSet { get; }
        object GetValue(bool beforeGet = true);
    }
    public interface IRXsProperty_Readonly<T> : IRXsProperty_Readonly
    {
        object IRXsProperty_Readonly.Value => Value;
        IRXsObservableImmediately<IRXsProperty_AfterSet> IRXsProperty_Readonly.AfterSet => AfterSet;
        object IRXsProperty_Readonly.GetValue(bool beforeGet) => GetValue(beforeGet);
        //
        new T Value { get; }
        new IRXsObservableImmediately<IRXsProperty_AfterSet<T>> AfterSet { get; }
        new T GetValue(bool beforeGet = true);
    }
}