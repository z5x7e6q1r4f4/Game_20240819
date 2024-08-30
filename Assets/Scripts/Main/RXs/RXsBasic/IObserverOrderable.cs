namespace Main.RXs
{
    public interface IObserverOrderable : IObserver
    {
        int Order { get; set; }
    }
    public interface IObserverOrderable<T> : IRXsObserver<T>, IObserverOrderable { }
}