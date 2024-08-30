using Main.RXs;
using System;
using UnityEngine;

namespace Main
{
    public class TimeNode :
        GameComponent,
        ITimeObservable,
        ITimeData
    {
        //Structur
        [field: SerializeField] public RXsProperty_SerializeField<TimeNode> Parent { get; } = new();
        [field: SerializeField] public RXsCollection_SerializeField<TimeNode> Children { get; } = new();
        //Property
        [field: SerializeField] public RXsProperty_SerializeField<float> Scale { get; } = new(1);
        [field: SerializeField] public RXsProperty_SerializeField<TimeState> State { get; } = new(TimeState.Play);
        public float Time { get; private set; }
        public float Delta { get; private set; }
        //Event
        private readonly RXsEventHandler<TimeNode> onUpdate = new();
        IDisposable IObservable<ITimeData>.Subscribe(IObserver<ITimeData> observer) => Subscribe(observer);
        public IDisposable Subscribe(IObserver<TimeNode> observer) => onUpdate.Subscribe(observer);
        public IDisposable Subscribe(IObserver observer) => this.SubscribeToTyped<TimeNode>(observer);
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