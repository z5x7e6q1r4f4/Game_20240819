using UnityEngine;
namespace Main
{   
    public class TimeManager : GameComponentSingleton<TimeManager>
    {
        public static TimeNode TimeNode => timeNode = timeNode != null ? timeNode : Instance.GetOrAddComponent<TimeNode>();
        private static TimeNode timeNode;
        private void Update()
        {
            TimeNode.UpdateTime(Time.deltaTime);
        }
    }
}