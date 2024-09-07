namespace Main
{
    public static partial class Operation
    {
        private static void AsOperatorOf(this IDisposableContainer operatorObserver, IDisposableContainer observer)
        {
            var subForObserver = observer.Add(sub => operatorObserver.Dispose());
            var subForOperator = operatorObserver.Add(sub => observer.RemoveAndDispose(sub));
        }
    }
}