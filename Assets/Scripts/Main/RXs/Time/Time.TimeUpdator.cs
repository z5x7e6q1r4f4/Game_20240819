using System;

namespace Main.RXs
{
    partial class Time
    {
        public class TimeUpdator : ITimeUpdator
        {
            public IObservableProperty<float> Time { get; } = new ObservableProperty_SerializeField<float>();
            public IObservableProperty<float> Delta { get; } = new ObservableProperty_SerializeField<float>();
            public IObservableProperty<float> Scale { get; } = new ObservableProperty_SerializeField<float>();
            public IObservableProperty<bool> IsPlaying { get; } = new ObservableProperty_SerializeField<bool>();
            IObservable<ITimeUpdator> ITimeUpdator.OnUpdate => OnUpdate;
            protected ObservableEventHandler<ITimeUpdator> OnUpdate { get; } = new();
            public void Update(float delta)
            {
                if (!IsPlaying.Value) return;
                Delta.Value = delta * Scale.Value;
                Time.Value += Delta.Value;
                OnUpdate.Invoke(this);
            }
            public TimeUpdator() => IsPlaying.AfterSet.Subscribe(e =>
            {
                if (!e.IsSame && !e.Current) { Delta.SetIfNotEqule(0); Update(0); }
            });
        }
    }
}