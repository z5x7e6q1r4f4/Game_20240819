using System;

namespace Main
{
    public static partial class Operation
    {
        private static void AsOperatorOf(this IDisposableHandler operatorObserver, IDisposableHandler observer)
        {
            observer.Add(operatorObserver);
            operatorObserver.Add(() => observer.RemoveAndDispose(operatorObserver));
        }
        private static void SubscribeOperator<T>(this IObservable<T> observable, IObserverDisposableHandler<T> operatorObserver)
        {
            var sub = observable.Subscribe(operatorObserver);
            operatorObserver.Add(sub);
        }
    }
}