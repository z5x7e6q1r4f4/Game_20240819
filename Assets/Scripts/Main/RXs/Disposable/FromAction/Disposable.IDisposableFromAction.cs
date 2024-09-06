using System;

namespace Main.RXs
{
    partial class Disposable
    {
        public interface IDisposableFromAction : IDisposableContainer
        {
            event Action<IDisposableFromAction> OnDisposeAction;
        }
    }
}