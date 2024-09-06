using System;

namespace Main.RXs
{
    partial class Time
    {
        public interface ITimeObservable : IObservable<ITimeData> { }
    }
}