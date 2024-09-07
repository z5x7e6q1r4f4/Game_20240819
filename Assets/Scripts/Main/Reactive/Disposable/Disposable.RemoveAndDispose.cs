using System;

namespace Main
{
    partial class Disposable 
    {
        public static void RemoveAndDispose(this IDisposableContainer disposableHandler, IDisposable disposable)
        {
            if (disposableHandler.Remove(disposable) &&
                disposableHandler.Count == 0)
                disposableHandler.Dispose();
        }
    }
}