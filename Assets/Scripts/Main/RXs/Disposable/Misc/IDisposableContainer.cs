using System;

namespace Main.RXs
{
    public interface IDisposableContainer : IDisposable
    {
        int Count { get; }
        void Add(IDisposable disposable);
        bool Remove(IDisposable disposable);
    }
}