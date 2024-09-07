using System;
using Main.RXs;

namespace Main
{
    public static partial class TimeAndUpdate
    {
        public static Timer GetTimer(
            this IObservable<ITimeData> timeObservable,
            float target = 0,
            float time = 0,
            float scale = 1,
            bool isPlaying = true)
        {
            var observer = Observer.Create<ITimeData>();
            var timer = Timer.GetFromReusePool(observer, target, time, scale, isPlaying);
            observer.OnNextAction += (_, timeData) => timer.Update(timeData.Delta);
            timeObservable.Subscribe(observer);
            return timer;
        }
        public static Timer GetTimer(
            this IObservable<ITimeData> timeObservable,
            float target,
            Action onArrive,
            float time = 0,
            float scale = 1,
            bool isPlaying = true)
        {
            var timer = GetTimer(timeObservable, target, time, scale, isPlaying);
            timer.OnArrive.Subscribe(onArrive);
            timer.Until(timer.OnArrive);
            return timer;
        }
    }
}