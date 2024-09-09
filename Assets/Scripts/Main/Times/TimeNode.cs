using System;
using UnityEngine;
namespace Main
{
    public class TimeNode : GameComponent, ITimeUpdator
    {
        public PropertySerializeField<TimeNode> Parent { get; private set; } = new();
        public CollectionSerializeField<TimeNode> Children { get; private set; } = new();
        IProperty<float> ITimeUpdator.Time => Time;
        [SerializeField] private PropertySerializeField<float> Time = new();
        IProperty<float> ITimeUpdator.Delta => Delta;
        [SerializeField] private PropertySerializeField<float> Delta = new();
        IProperty<float> ITimeUpdator.Scale => Scale;
        [SerializeField] private PropertySerializeField<float> Scale = new();
        IProperty<bool> ITimeUpdator.IsPlaying => IsPlaying;
        [SerializeField] private PropertySerializeField<bool> IsPlaying = new();
        IObservable<ITimeUpdator> ITimeUpdator.OnUpdate => OnUpdate;
        public IObservable<TimeNode> OnUpdate => OnUpdateHandler;
        private EventHandler<TimeNode> OnUpdateHandler { get; } = new();
        protected void UpdateTime(float delta)
        {
            if (!IsPlaying.Value) return;
            Delta.Value = delta * Scale.Value;
            Time.Value += Delta.Value;
            OnUpdateHandler.Invoke(this);
            foreach (var child in Children) child.UpdateTime(Delta.Value);
        }
        protected override void OnGameComponentAwake()
        {
            IsPlaying.AfterSet.Subscribe(e =>
            {
                if (e.IsSame || e.Current) return;
                Delta.SetIfNotEquals(0);
                foreach (var child in Children) child.UpdateTime(Delta.Value);
            }).Until(OnGameComponentDestroyEvent);
            Parent.LinkCollection(this, timeNode => timeNode.Children).Until(OnGameComponentDestroyEvent);
            Children.LinkItem(this, timeNode => timeNode.Parent).Until(OnGameComponentDestroyEvent);
        }
    }
}