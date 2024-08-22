using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public static partial class RXsValueOperatorExtension
    {
        public interface IOperatorToCollection<T> : IDisposable, IRXsCollection_Readonly<T> { }
        public interface IOperatorToProperty<T> : IDisposable, IRXsProperty_Readonly<T> { }
    }
}