namespace Main.RXs
{
    public interface IRXsProperty_BeforeSet : IRXsProperty_BeforeGet
    {
        bool IsEnable { get; set; }
    }
    public interface IRXsProperty_BeforeSet<T> : IRXsProperty_BeforeGet<T>, IRXsProperty_BeforeSet
    { }
}