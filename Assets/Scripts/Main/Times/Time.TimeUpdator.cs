using System;
using Main.RXs;

namespace Main
{
    partial class TimeAndUpdate
    {
        public class TimeUpdator : ITimeUpdator
        {
            public IObservableProperty<float> Time { get; } = new ObservableProperty_SerializeField<float>();
            public IObservableProperty<float> Delta { get; } = new ObservableProperty_SerializeField<float>();
            public IObservableProperty<float> Scale { get; } = new ObservableProperty_SerializeField<float>();
            public IObservableProperty<bool> IsPlaying { get; } = new ObservableProperty_SerializeField<bool>();
            public IObservable<ITimeUpdator> OnUpdate => OnUpdateHandler;
            protected EventHandler<ITimeUpdator> OnUpdateHandler { get; } = new();
            public virtual void Update(float delta)
            {
                if (!IsPlaying.Value) return;
                Delta.Value = delta * Scale.Value;
                Time.Value += Delta.Value;
                OnUpdateHandler.Invoke(this);
            }
            public TimeUpdator() => IsPlaying.AfterSet.Subscribe(e =>
            {
                if (!e.IsSame && !e.Current) { Delta.SetIfNotEqule(0); Update(0); }
            });
        }
    }
}