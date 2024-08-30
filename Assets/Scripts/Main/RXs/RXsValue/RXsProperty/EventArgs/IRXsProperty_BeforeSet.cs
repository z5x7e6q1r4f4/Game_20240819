namespace Main.RXs
{
    public interface IRXsProperty_BeforeSet : IRXsProperty_AfterSet
    {
        bool IsEnable { get; set; }
        object Modified { get; set; }
    }
    public interface IRXsProperty_BeforeSet<T> : IRXsProperty_AfterSet<T>, IRXsProperty_BeforeSet
    {
        object IRXsProperty_BeforeSet.Modified { get => Modified; set => Modified = (T)value; }
        new T Modified { get; set; }
    }
}