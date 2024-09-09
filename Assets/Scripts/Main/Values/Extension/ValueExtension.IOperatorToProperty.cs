using System;

namespace Main
{
    partial class ValueExtension
    {
        public interface IOperatorToProperty<T> : IPropertyReadonly<T>, IDisposable { }
    }
}