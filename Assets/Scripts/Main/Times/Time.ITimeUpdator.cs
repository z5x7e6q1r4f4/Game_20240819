using System;
using Main.RXs;

namespace Main
{
    partial class TimeAndUpdate
    {
        public interface ITimeUpdator : ITimeData, IObservable<ITimeData>
        {
            IDisposable IObservable<ITimeData>.Subscribe(IObserver<ITimeData> observer) => OnUpdate.Subscribe(observer);
            float ITimeData.Time => Time.Value;
            float ITimeData.Delta => Delta.Value;
            new IObservableProperty<float> Time { get; }
            new IObservableProperty<float> Delta { get; }
            IObservableProperty<float> Scale { get; }
            IObservableProperty<bool> IsPlaying { get; }
            IObservable<ITimeUpdator> OnUpdate { get; }
        }
    }
}