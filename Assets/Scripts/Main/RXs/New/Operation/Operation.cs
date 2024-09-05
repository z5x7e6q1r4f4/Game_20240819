using Mono.Cecil;
using System;

namespace Main.RXs
{
    public static partial class Operation
    {
        private static void AsOperatorOf(this ISubscriptionHandler @operator, ISubscriptionHandler observer)
        {
            var subForObserver = observer.Add(sub => @operator.Dispose());
            var subForOperator = @operator.Add(sub => observer.RemoveAndDispose(sub));
        }
    }
}