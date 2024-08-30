namespace Main.RXs.Collection
{
    public interface IRXsCollection_EventArgs_ModifyBase :
        IRxsCollection_EventArgs_Base
    {
        bool IsEnable { get; set; }
    }
    public interface IRXsCollection_EventArgs_ModifyBase<T> :
        IRXsCollection_EventArgs_ModifyBase,
        IRxsCollection_EventArgs_Base<T>
    { }
}