using Main.RXs;

namespace Main
{
    public interface ITimeData
    {
        float Time { get; }
        float Delta { get; }
    }
    public interface ITimeObservable
    {
        IObservable<ITimeData> OnNext { get; }
    }
    public interface ITimeObserver : IObserver<ITimeData> { }
}