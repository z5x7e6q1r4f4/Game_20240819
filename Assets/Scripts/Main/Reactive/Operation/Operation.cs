using System;

namespace Main
{
    public static partial class Operation
    {
        private static void AsOperatorOf(this IDisposableHandler operatorObserver, IDisposableHandler observer)
        {
            observer.Add(operatorObserver);
            operatorObserver.Add(() => observer.Remove(operatorObserver));
        }
        private static IDisposable SubscribeOperator<T>(this IObservable<T> observable, IObserverDisposableHandler<T> operatorObserver)
        {
            var disposable = observable.Subscribe(operatorObserver);
            operatorObserver.Add(disposable);
            return operatorObserver;
        }
    }
}