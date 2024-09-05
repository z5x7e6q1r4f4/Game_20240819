using System;

namespace Main.RXs
{
    public static partial class Disposables
    {
        public interface IDisposableFromAction : IDisposable
        {
            event Action<IDisposableFromAction> OnDisposeAction;
        }
    }
}