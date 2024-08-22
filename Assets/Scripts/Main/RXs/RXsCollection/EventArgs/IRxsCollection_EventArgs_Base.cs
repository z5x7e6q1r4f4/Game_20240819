namespace Main.RXs.Collection
{
    public interface IRxsCollection_EventArgs_Base
    {
        IRXsCollection_Readonly Collection { get; }
        int Index { get; }
        object Item { get; }
    }
    public interface IRxsCollection_EventArgs_Base<T> : IRxsCollection_EventArgs_Base
    {
        IRXsCollection_Readonly IRxsCollection_EventArgs_Base.Collection => Collection;
        object IRxsCollection_EventArgs_Base.Item => Item;
        //
        new IRXsCollection_Readonly<T> Collection { get; }
        new T Item { get; }
    }
}