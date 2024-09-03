namespace Main.RXs
{
    public interface IRXsProperty_BeforeGet: IRXsProperty_AfterSet 
    {
        object Modified { get; set; }
    }
    public interface IRXsProperty_BeforeGet<T> : IRXsProperty_AfterSet<T>, IRXsProperty_BeforeGet
    {
        object IRXsProperty_BeforeGet.Modified { get => Modified; set => Modified = (T)value; }
        new T Modified { get; set; }
    }
}