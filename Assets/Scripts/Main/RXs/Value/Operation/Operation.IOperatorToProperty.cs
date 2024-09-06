using System;

namespace Main.RXs
{
    partial class Operation
    {
        public interface IOperatorToProperty<T> : IObservableProperty_Readonly<T>, IDisposable { }
    }
}