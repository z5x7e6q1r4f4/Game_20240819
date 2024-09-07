using System;

namespace Main
{
    public static partial class Operation
    {
        private static IObserverDisposableHandler<T> AsOperatorOf<T>(this IObserverDisposableHandler<T> operatorObserver, IDisposableHandler observer)
        {
            observer.Add(operatorObserver);
            operatorObserver.Add(() => observer.Remove(operatorObserver));
            if (observer is IObserverOrderable orderable) operatorObserver.Order = orderable.Order;
            return operatorObserver;
        }
        private static IDisposable SubscribeOperator<T>(this IObservable<T> observable, IObserverDisposableHandler<T> operatorObserver)
        {
            var disposable = observable.Subscribe(operatorObserver);
            operatorObserver.Add(disposable);
            return operatorObserver;
        }
    }
}