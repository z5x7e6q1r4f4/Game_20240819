using System;

namespace Main.RXs
{
    partial class Time
    {
        public interface ITimeObserver : IObserverDisposableHandler<ITimeData> { }
    }
}