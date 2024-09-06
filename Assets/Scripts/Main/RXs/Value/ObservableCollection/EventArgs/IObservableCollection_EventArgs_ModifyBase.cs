namespace Main.RXs.ObservableCollections
{
    public interface IObservableCollection_EventArgs_ModifyBase :
        IObservableCollection_EventArgs_Base
    {
        bool IsEnable { get; set; }
    }
    public interface IObservableCollection_EventArgs_ModifyBase<T> :
        IObservableCollection_EventArgs_ModifyBase,
        IObservableCollection_EventArgs_Base<T>
    { }
}