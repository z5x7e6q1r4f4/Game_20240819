using System;

namespace Main
{
    public interface IDisposableContainer : IDisposable
    {
        int Count { get; }
        void Add(IDisposable disposable);
        bool Remove(IDisposable disposable);
    }
}