using System;

namespace Main
{
    public interface IDisposableBase : IDisposable
    {
        event Action<IDisposableBase> OnDisposeAction;
    }
}