using System;

namespace Main
{
    public interface IDisposableHandler : IDisposable
    {
        int Count { get; }
        void Add(IDisposable disposable);
        bool Remove(IDisposable disposable);
    }
}