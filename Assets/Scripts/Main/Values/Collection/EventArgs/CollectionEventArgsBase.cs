namespace Main
{
    public interface CollectionEventArgsBase
    {
        CollectionEventArgsType Type { get; }
        ICollectionReadonly Collection { get; }
        int Index { get; }
        object Item { get; }
    }
    public interface CollectionEventArgsBase<T> : CollectionEventArgsBase
    {
        ICollectionReadonly CollectionEventArgsBase.Collection => Collection;
        object CollectionEventArgsBase.Item => Item;
        //
        new ICollectionReadonly<T> Collection { get; }
        new T Item { get; }
    }
}