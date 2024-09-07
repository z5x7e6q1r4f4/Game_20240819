using System;

namespace Main
{
    partial class Observable
    {
        public static IObservableDisposable<Timer> Every(float every, ITimeUpdator timeUpdator = null)
        {
            timeUpdator ??= Updates.TimeNode;
            return Create<Timer>((observable, observer) =>
            {
                var timer = timeUpdator.GetTimer(every);
                timer.OnArrive.Subscribe(timer =>
                {
                    timer.Restart();
                    observer.OnNext(timer);
                });
                var disposable = Disposable.Create(timer);
                observer.Add(disposable);
                disposable.Add(() => observer.Remove(disposable));
                return disposable;
            });
        }
    }
}