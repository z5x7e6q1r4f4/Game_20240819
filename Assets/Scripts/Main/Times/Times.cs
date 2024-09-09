using System;

namespace Main
{
    public static partial class Times
    {
        public static Timer GetTimer(
            this IObservable<ITimeData> timeObservable,
            float target = 0,
            float time = 0,
            float scale = 1,
            bool isPlaying = true)
        {
            var timer = Timer.GetFromReusePool(timeObservable, target, time, scale, isPlaying);
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
            timer.WithDispose(timer.OnArrive.Subscribe(onArrive));
            timer.WithDispose(timer.Until(timer.OnArrive, 0));
            return timer;
        }
        //
        public static IDisposable Until(this IDisposable disposable, float until, IObservable<ITimeData> timeUpdator = null)
        {
            timeUpdator ??= Updates.TimeNode;
            return timeUpdator.GetTimer(until, disposable.Dispose);
        }
        public static IObservable<ITimeData> Every(this IObservable<ITimeData> timeUpdator, float every, bool autoDispose = true)
        {
            return Observable.Create<Timer>((observable, observer) =>
            {
                var timer = timeUpdator.GetTimer(every);
                timer.OnArrive.Subscribe(timer =>
                {
                    timer.Restart();
                    observer.OnNext(timer);
                });
                timer.AsOperatorOf(observer);
                if (autoDispose) observable.Dispose();
                return observable.SubscribeOperator(timer);
            });
        }
        public static IObservable<T> Delay<T>(this IObservable<T> observable, float delay, ITimeUpdator timeUpdator = null, bool autoDispose = true)
        {
            timeUpdator ??= Updates.TimeNode;
            return Observable.Create<T>((operatorObservable, observer) =>
            {
                var disposeList = Reuse.ReuseList<IDisposable>.Get();
                var operatorObserver = Observer.Create<T>(e =>
                {
                    disposeList.Add(timeUpdator.GetTimer(delay, () => observer.OnNext(e)));
                },
                onDispose: () =>
                {
                    foreach (var dispose in disposeList) dispose.Dispose();
                    disposeList.Dispose();
                });
                if (autoDispose) operatorObservable.Dispose();
                return observable.SubscribeOperator(operatorObserver.AsOperatorOf(observer));
            });
        }
    }
}