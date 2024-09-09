using System;

namespace Main
{
    public static partial class Operation
    {
        public static IObserverBase<TOperator> AsOperatorOf<TOperator, TObserver>(this IObserverBase<TOperator> operatorObserver, IObserverBase<TObserver> observer)
        {
            observer.AddSubscription(operatorObserver);
            operatorObserver.WhenDispose(() => observer.RemoveSubscription(operatorObserver));
            operatorObserver.Order = observer.Order;
            return operatorObserver;
        }
        public static IDisposable SubscribeOperator<T>(this IObservable<T> observable, IObserverBase<T> operatorObserver)
        {
            var disposable = observable.Subscribe(operatorObserver);
            operatorObserver.WithDispose(disposable);
            return operatorObserver;
        }
    }
}