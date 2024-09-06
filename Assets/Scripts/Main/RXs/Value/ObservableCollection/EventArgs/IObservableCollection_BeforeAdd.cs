using Main.RXs.ObservableCollections;

namespace Main.RXs
{
    public interface IObservableCollection_BeforeAdd : IObservableCollection_EventArgs_ModifyBase
    { object Modified { get; set; } }
    public interface IObservableCollection_BeforeAdd<T> : IObservableCollection_EventArgs_ModifyBase<T>, IObservableCollection_BeforeAdd
    {
        object IObservableCollection_BeforeAdd.Modified { get => Modified; set => Modified = (T)value; }
        //
        new T Modified { get; set; }
    }
}