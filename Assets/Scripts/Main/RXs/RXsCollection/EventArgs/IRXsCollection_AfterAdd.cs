using Main.RXs.Collection;

namespace Main.RXs
{
    public interface IRXsCollection_AfterAdd : IRxsCollection_EventArgs_Base { }
    public interface IRXsCollection_AfterAdd<T> : IRxsCollection_EventArgs_Base<T>, IRXsCollection_AfterAdd { }
}