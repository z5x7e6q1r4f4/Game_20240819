namespace Main.RXs.ObservableCollections
{
    public interface IObservableCollection_EventArgs_Base
    {
        ObservableCollectionEventArgsType Type { get; }
        IObservableCollection_Readonly Collection { get; }
        int Index { get; }
        object Item { get; }
    }
    public interface IObservableCollection_EventArgs_Base<T> : IObservableCollection_EventArgs_Base
    {
        IObservableCollection_Readonly IObservableCollection_EventArgs_Base.Collection => Collection;
        object IObservableCollection_EventArgs_Base.Item => Item;
        //
        new IObservableCollection_Readonly<T> Collection { get; }
        new T Item { get; }
    }
}