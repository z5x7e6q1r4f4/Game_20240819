namespace Main.RXs
{
    internal static class ObserverListSubscriptionExtension
    {
        private class ObserverListSubscriptionObserver<T> : ObserverListSubscription<T>
        {
            private readonly System.IObserver<T> observer;
            protected override void OnCompleted() => observer.OnCompleted();
            protected override void OnError(System.Exception error) => observer.OnError(error);
            protected override void OnNext(T value) => observer.OnNext(value);
            public ObserverListSubscriptionObserver(System.IObserver<T> observer) => this.observer = observer;
        }
        public static IObserverListSubscription<T> ToSubscription<T>(this System.IObserver<T> observer)
        {
            if (observer is not IObserverListSubscription<T> subscription) subscription = new ObserverListSubscriptionObserver<T>(observer);
            return subscription;
        }
    }
}