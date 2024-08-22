using System;

namespace Main.RXs
{
    public static partial class Disposiable
    {
        public static IDisposable Empty = new EmptyDisposable();
        private class EmptyDisposable : IDisposable
        { void IDisposable.Dispose() { } }
    }
}