using Main.RXs.RXsCollections;

namespace Main.RXs
{
    public interface IRXsCollection_BeforeAdd : IRXsCollection_EventArgs_ModifyBase
    { object Modified { get; set; } }
    public interface IRXsCollection_BeforeAdd<T> : IRXsCollection_EventArgs_ModifyBase<T>, IRXsCollection_BeforeAdd
    {
        object IRXsCollection_BeforeAdd.Modified { get => Modified; set => Modified = (T)value; }
        //
        new T Modified { get; set; }
    }
}