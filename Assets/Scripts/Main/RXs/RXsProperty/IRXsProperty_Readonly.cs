namespace Main.RXs
{
    public interface IRXsProperty_Readonly
    {
        object Value { get; }
        IObservableImmediately<IRXsProperty_AfterSet> AfterSet { get; }
    }
    public interface IRXsProperty_Readonly<T> : IRXsProperty_Readonly
    {
        object IRXsProperty_Readonly.Value => Value;
        IObservableImmediately<IRXsProperty_AfterSet> IRXsProperty_Readonly.AfterSet => AfterSet;
        //
        new T Value { get; }
        new IObservableImmediately<IRXsProperty_AfterSet<T>> AfterSet { get; }
    }
}