using System;

namespace Main
{
    partial class ValueExtension
    {
        public interface IOperatorToCollection<T> : ICollectionReadonly<T>, IDisposable { }
    }
}