using Main.RXs.RXsProperties;

namespace Main.RXs
{
    public interface IObservableProperty_BeforeGet
    {
        ObservablePropertyEventArgsType Type { get; }
        IObservableProperty_Readonly Property { get; }
        object Previous { get; }
        object Modified { get; set; }
    }
    public interface IObservableProperty_BeforeGet<T> : IObservableProperty_BeforeGet
    {
        IObservableProperty_Readonly IObservableProperty_BeforeGet.Property => Property;
        object IObservableProperty_BeforeGet.Modified { get => Modified; set => Modified = (T)value; }
        object IObservableProperty_BeforeGet.Previous => Previous;
        new IObservableProperty_Readonly<T> Property { get; }
        new T Previous { get; }
        new T Modified { get; set; }
    }
}