namespace Main.RXs
{
    partial class Operation
    {
        public interface IRXsOperatorToProperty<T> : IRXsProperty_Readonly<T>, IRXsDisposable { }
    }
}