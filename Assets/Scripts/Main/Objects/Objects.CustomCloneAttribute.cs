using System;

namespace Main
{
    public static partial class Objects
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
        public abstract class CustomCloneAttribute : Attribute
        {
            public abstract T Clone<T>(T item, params object[] args);
        }
    }
}
