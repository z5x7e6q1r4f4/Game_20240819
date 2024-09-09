namespace Main  
{
    public interface CollectionEventArgsModifyBase :
        CollectionEventArgsBase
    {
        bool IsEnable { get; set; }
    }
    public interface CollectionEventArgsModifyBase<T> :
        CollectionEventArgsModifyBase,
        CollectionEventArgsBase<T>
    { }
}