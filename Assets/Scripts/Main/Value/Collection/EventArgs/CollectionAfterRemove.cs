namespace Main
{
    public interface CollectionAfterRemove : CollectionEventArgsBase { }
    public interface CollectionAfterRemove<T> : CollectionEventArgsBase<T>, CollectionAfterRemove { }
}