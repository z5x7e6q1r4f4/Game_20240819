using System;

namespace Main.RXs
{
    partial class Operation
    {
        public interface IOperatorToCollection<T> : IObservableCollection_Readonly<T>, IDisposable { }
    }
}