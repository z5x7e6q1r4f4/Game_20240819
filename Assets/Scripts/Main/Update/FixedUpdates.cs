using System;
using UnityEngine;
namespace Main
{
    public class FixedUpdates : TimeNodeSingleton<FixedUpdates>
    {
        public static IObservable<TimeNode> Event => TimeNode.OnUpdate;
        private void FixedUpdate() => UpdateTime(Time.fixedDeltaTime);
    }
}