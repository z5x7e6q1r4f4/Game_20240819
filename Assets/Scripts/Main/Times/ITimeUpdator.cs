using System;

namespace Main
{
    public interface ITimeUpdator : ITimeData, IObservable<ITimeData>
    {
        IDisposable IObservable<ITimeData>.Subscribe(IObserver<ITimeData> observer) => OnUpdate.Subscribe(observer);
        float ITimeData.Time => Time.Value;
        float ITimeData.Delta => Delta.Value;
        new IProperty<float> Time { get; }
        new IProperty<float> Delta { get; }
        IProperty<float> Scale { get; }
        IProperty<bool> IsPlaying { get; }
        IObservable<ITimeUpdator> OnUpdate { get; }
    }
}