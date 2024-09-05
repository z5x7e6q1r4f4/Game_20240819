namespace Main.RXs
{
    partial class Operation
    {
        public interface IRXsOperatorToCollection<T> : IRXsCollection_Readonly<T>, IRXsDisposable { }
    }
}