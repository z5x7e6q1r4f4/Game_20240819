using NSubstitute;
using System;

namespace Main
{
    partial class Disposable
    {
        public static T WhenDispose<T>(this T disposableBase, Action<IDisposableBase> onDispose)
             where T : IDisposableBase
        {
            disposableBase.OnDisposeAction += onDispose;
            return disposableBase;
        }
        public static T WhenDispose<T>(this T disposableBase, Action onDispose) where T : IDisposableBase => disposableBase.WhenDispose(_ => onDispose());
        public static T WithDispose<T>(this T disposableBase, IDisposable disposable) where T : IDisposableBase => disposableBase.WhenDispose(disposable.Dispose);
    }
}