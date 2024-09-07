namespace Main
{
    public interface CollectionBeforeRemove : CollectionEventArgsModifyBase { }
    public interface CollectionBeforeRemove<T> : CollectionEventArgsModifyBase<T>, CollectionBeforeRemove { }
}