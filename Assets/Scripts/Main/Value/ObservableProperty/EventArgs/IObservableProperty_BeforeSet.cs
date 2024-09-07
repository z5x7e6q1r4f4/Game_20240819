using Main.RXs.RXsProperties;

namespace Main.RXs
{
    public interface IObservableProperty_BeforeSet
    {
        ObservablePropertyEventArgsType Type { get; }
        IObservableProperty_Readonly Property { get; }
        bool IsEnable { get; set; }
        bool IsSame { get; }
        object Previous { get; }
        object Current { get; }
        object Modified { get; set; }
    }
    public interface IObservableProperty_BeforeSet<T> : IObservableProperty_BeforeSet
    {
        IObservableProperty_Readonly IObservableProperty_BeforeSet.Property => Property;
        bool IObservableProperty_BeforeSet.IsSame => Equals(Previous, Current);
        object IObservableProperty_BeforeSet.Previous => Previous;
        object IObservableProperty_BeforeSet.Current => Current;
        object IObservableProperty_BeforeSet.Modified { get => Modified; set => Modified = (T)value; }
        //
        new IObservableProperty_Readonly<T> Property { get; }
        new T Previous { get; }
        new T Current { get; }
        new T Modified { get; set; }
    }
}