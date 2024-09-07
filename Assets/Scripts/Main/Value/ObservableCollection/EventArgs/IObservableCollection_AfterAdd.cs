using Main.RXs.ObservableCollections;

namespace Main.RXs
{
    public interface IObservableCollection_AfterAdd : IObservableCollection_EventArgs_Base { }
    public interface IObservableCollection_AfterAdd<T> : IObservableCollection_EventArgs_Base<T>, IObservableCollection_AfterAdd { }
}