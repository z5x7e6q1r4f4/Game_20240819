using System;

namespace Main
{
    public static partial class Objects
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
        public abstract class CustomNewAttribute : Attribute
        {
            public abstract T New<T>(Type type, params object[] args);
        }
    }
}
