using System;

namespace Main.RXs
{
    partial class Observer
    {
        public static IDisposable Subscribe<T>(
            this IObservable<T> observable,
            Action<T> onNext = null,
            Action onCompleted = null,
            Action<Exception> onError = null,
            Action onDispose = null)
        => observable.Subscribe(Create(onNext, onCompleted, onError, onDispose));
        public static IDisposable Subscribe<T>(
            this IObservable<T> observable,
            Action onNext = null,
            Action onCompleted = null,
            Action onError = null,
            Action onDispose = null)
        => observable.Subscribe(Create<T>(onNext, onCompleted, onError, onDispose));
    }
}