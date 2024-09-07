using System;
using UnityEngine;
namespace Main
{
    public class Updates : TimeNodeSingleton<Updates>
    {
        public static IObservable<TimeNode> Event => TimeNode.OnUpdate;
        private void Update() => UpdateTime(Time.deltaTime);
    }
}