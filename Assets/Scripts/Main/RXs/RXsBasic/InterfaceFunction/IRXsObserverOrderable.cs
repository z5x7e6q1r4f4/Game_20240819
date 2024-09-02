namespace Main.RXs
{
    public interface IRXsObserverOrderable : IRXsObserver
    {
        int Order { get; set; }
    }
    public interface IRXsObserverOrderable<in T> : IRXsObserver<T>, IRXsObserverOrderable { }
}