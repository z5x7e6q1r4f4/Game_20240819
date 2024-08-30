using Main.RXs.Collection;

namespace Main.RXs
{
    public interface IRXsCollection_BeforeRemove : IRXsCollection_EventArgs_ModifyBase { }
    public interface IRXsCollection_BeforeRemove<T> : IRXsCollection_EventArgs_ModifyBase<T>, IRXsCollection_BeforeRemove { }
}