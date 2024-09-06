using Main.RXs.ObservableCollections;

namespace Main.RXs
{
    public interface IObservableCollection_BeforeRemove : IObservableCollection_EventArgs_ModifyBase { }
    public interface IObservableCollection_BeforeRemove<T> : IObservableCollection_EventArgs_ModifyBase<T>, IObservableCollection_BeforeRemove { }
}