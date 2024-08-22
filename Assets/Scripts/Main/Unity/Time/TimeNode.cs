using Codice.Client.BaseCommands;
using Main.RXs;
using UnityEngine;

namespace Main
{
    public enum TimeState : int
    {
        Play = 1,
        Stop = 0
    }
    public class TimeNode : GameComponent, ITimeData, ITimeObservable
    {
        //Structur
        [field: SerializeField] public RXsProperty_SerializeField<TimeNode> Parent { get; } = new();
        [field: SerializeField] public RXsCollection_SerializeField<TimeNode> Children { get; } = new();
        //Property
        [field: SerializeField] public RXsProperty_SerializeField<float> Scale { get; } = new(1);
        [field: SerializeField] public RXsProperty_SerializeField<TimeState> State { get; } = new(TimeState.Play);
        public float Time { get; private set; }
        public float Delta { get; private set; }
        ////Event
        IObservable<ITimeData> ITimeObservable.OnNext => onUpdate;
        public IObservable<TimeNode> OnUpdate => onUpdate;

        private readonly RXsEventHandler<TimeNode> onUpdate = new();
        protected override void OnGameComponentAwake()
        {
            Parent.LinkCollection(this, timeNode => timeNode.Children);
            Children.LinkItem(this, timeNode => timeNode.Parent);
        }
        public void UpdateTime(float deltaTime)
        {
            Delta = deltaTime * Scale.Value * (int)TimeState.Play;
            Time += Delta;
            foreach (var child in Children) child.UpdateTime(deltaTime);
            if (State.Value == TimeState.Play) onUpdate.Invoke(this);
        }
    }
}