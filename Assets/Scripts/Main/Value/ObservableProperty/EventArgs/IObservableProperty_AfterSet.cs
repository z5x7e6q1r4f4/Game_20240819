using Main.RXs.RXsProperties;

namespace Main.RXs
{
    public interface IObservableProperty_AfterSet
    {
        ObservablePropertyEventArgsType Type { get; }
        IObservableProperty_Readonly Property { get; }
        bool IsSame { get; }
        object Previous { get; }
        object Current { get; }
    }
    public interface IObservableProperty_AfterSet<T> : IObservableProperty_AfterSet
    {
        IObservableProperty_Readonly IObservableProperty_AfterSet.Property => Property;
        bool IObservableProperty_AfterSet.IsSame => Equals(Previous, Current);
        object IObservableProperty_AfterSet.Previous => Previous;
        object IObservableProperty_AfterSet.Current => Current;
        //
        new IObservableProperty_Readonly<T> Property { get; }
        new T Previous { get; }
        new T Current { get; }
    }
}