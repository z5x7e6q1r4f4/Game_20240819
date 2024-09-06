namespace Main.RXs
{
    public interface IObservableProperty_Readonly
    {
        object Value { get; }
        IObservableImmediately<IObservableProperty_AfterSet> AfterSet { get; }
        object GetValue(bool beforeGet = true);
    }
    public interface IObservableProperty_Readonly<T> : IObservableProperty_Readonly
    {
        object IObservableProperty_Readonly.Value => Value;
        IObservableImmediately<IObservableProperty_AfterSet> IObservableProperty_Readonly.AfterSet => AfterSet;
        object IObservableProperty_Readonly.GetValue(bool beforeGet) => GetValue(beforeGet);
        //
        new T Value { get; }
        new IObservableImmediately<IObservableProperty_AfterSet<T>> AfterSet { get; }
        new T GetValue(bool beforeGet = true);
    }
}