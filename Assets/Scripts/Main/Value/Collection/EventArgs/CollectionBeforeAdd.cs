namespace Main
{
    public interface CollectionBeforeAdd : CollectionEventArgsModifyBase
    { object Modified { get; set; } }
    public interface CollectionBeforeAdd<T> : CollectionEventArgsModifyBase<T>, CollectionBeforeAdd
    {
        object CollectionBeforeAdd.Modified { get => Modified; set => Modified = (T)value; }
        //
        new T Modified { get; set; }
    }
}