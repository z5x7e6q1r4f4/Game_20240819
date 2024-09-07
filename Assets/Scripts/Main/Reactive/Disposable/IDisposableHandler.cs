using System;

namespace Main
{
    public interface IDisposableHandler : IDisposable
    {
        void Add(IDisposable disposable);
        void Remove(IDisposable disposable, bool tryDispose = true);
    }
}