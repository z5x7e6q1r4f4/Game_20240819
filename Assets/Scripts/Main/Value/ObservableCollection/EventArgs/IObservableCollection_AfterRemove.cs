using Main.RXs.ObservableCollections;

namespace Main.RXs
{
    public interface IObservableCollection_AfterRemove : IObservableCollection_EventArgs_Base { }
    public interface IObservableCollection_AfterRemove<T> : IObservableCollection_EventArgs_Base<T>, IObservableCollection_AfterRemove { }
}