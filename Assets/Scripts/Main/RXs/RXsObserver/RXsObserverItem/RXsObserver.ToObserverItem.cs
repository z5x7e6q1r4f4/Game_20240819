namespace Main.RXs
{
    partial class RXsObserver
    {
        private class ObserverListSubscriptionObserver<T> : RXsObserverItem<T>
        {
            private readonly System.IObserver<T> observer;
            protected override void OnCompleted() => observer.OnCompleted();
            protected override void OnError(System.Exception error) => observer.OnError(error);
            protected override void OnNext(T value) => observer.OnNext(value);
            public ObserverListSubscriptionObserver(System.IObserver<T> observer) => this.observer = observer;
        }
        public static IRXsObserverItem<T> ToObserverItem<T>(this System.IObserver<T> observer)
        {
            if (observer is not IRXsObserverItem<T> subscription) subscription = new ObserverListSubscriptionObserver<T>(observer);
            return subscription;
        }
    }
}