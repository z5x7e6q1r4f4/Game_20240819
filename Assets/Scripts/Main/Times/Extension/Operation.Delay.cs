using System;

namespace Main
{
    partial class Operation
    {
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