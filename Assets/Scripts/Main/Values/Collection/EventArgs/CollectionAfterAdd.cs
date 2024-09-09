namespace Main
{
    public interface CollectionAfterAdd : CollectionEventArgsBase { }
    public interface CollectionAfterAdd<T> : CollectionEventArgsBase<T>, CollectionAfterAdd { }
}