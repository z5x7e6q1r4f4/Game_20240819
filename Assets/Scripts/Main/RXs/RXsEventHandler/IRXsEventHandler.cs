using System;

namespace Main.RXs
{
    public interface IRXsEventHandler<T> : IRXsObservable<T>
    {
        void Invoke(T eventArgs);
        void Clear();
    }
}