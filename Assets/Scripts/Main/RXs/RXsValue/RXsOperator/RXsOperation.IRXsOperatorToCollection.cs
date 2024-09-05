namespace Main.RXs
{
    partial class RXsOperation
    {
        public interface IRXsOperatorToCollection<T> : IRXsCollection_Readonly<T>, IRXsDisposable { }
    }
}