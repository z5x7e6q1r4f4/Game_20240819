using System;

namespace Main
{
    partial class Disposable
    {
        public static IDisposable Until(this IDisposable disposable, float until, ITimeUpdator timeUpdator = null)
        {
            timeUpdator ??= Updates.TimeNode;
            return timeUpdator.GetTimer(until, disposable.Dispose);
        }
    }
}