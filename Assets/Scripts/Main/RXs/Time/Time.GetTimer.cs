using System;

namespace Main.RXs
{
    partial class Time
    {
        public static Timer GetTimer(
            this ITimeObservable timeObservable,
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
            this ITimeObservable timeObservable,
            float target,
            Action onArrive,
            float time = 0,
            float scale = 1,
            bool isPlaying = true)
        {
            var timer = GetTimer(timeObservable, target, time, scale, isPlaying);
            timer.Until(timer.OnArrive);
            return timer;
        }
    }
}