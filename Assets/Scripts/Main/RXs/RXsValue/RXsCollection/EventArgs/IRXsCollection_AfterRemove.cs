using Main.RXs.RXsCollections;

namespace Main.RXs
{
    public interface IRXsCollection_AfterRemove : IRxsCollection_EventArgs_Base { }
    public interface IRXsCollection_AfterRemove<T> : IRxsCollection_EventArgs_Base<T>, IRXsCollection_AfterRemove { }
}