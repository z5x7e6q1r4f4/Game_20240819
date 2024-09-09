using System;

namespace Main
{
    public sealed class Timer :
        ObserverBaseReuseable<Timer, ITimeData>,
        ITimeUpdator
    {
        public static Timer GetFromReusePool(IObservable<ITimeData> observable, float target = 0, float time = 0, float scale = 1, bool isPlaying = true)
        {
            var timer = GetFromReusePool(false);
            timer.Target.Value = target;
            timer.Time.Value = time;
            timer.Scale.Value = scale;
            timer.IsPlaying.Value = isPlaying;
            //
            timer.WithDispose(timer.IsPlaying.AfterSet.Immediately().Subscribe(e =>
            {
                if (e.IsSame || e.Current) return;
                timer.Delta.SetIfNotEquals(0);
            }));
            timer.WithDispose(timer.Time.LimitMin(0));
            timer.WithDispose(timer.Time.LimitMax(timer.Target));
            timer.WithDispose(timer.Target.LimitMin(0));
            observable.Subscribe(timer);
            return timer;
        }
        protected override void OnRelease()
        {
            OnUpdateHandler.Clear();
            OnArriveHandler.Clear();
            Delta.Value = 0;
            base.OnRelease();
        }
        //
        public IProperty<float> Time { get; } = new PropertySerializeField<float>();
        public IProperty<float> Delta { get; } = new PropertySerializeField<float>();
        public IProperty<float> Scale { get; } = new PropertySerializeField<float>();
        public IProperty<bool> IsPlaying { get; } = new PropertySerializeField<bool>();
        public PropertySerializeField<float> Target = new();
        IObservable<ITimeUpdator> ITimeUpdator.OnUpdate => OnUpdate;
        public IObservable<Timer> OnUpdate => OnUpdateHandler;
        private EventHandler<Timer> OnUpdateHandler { get; } = new();
        public IObservable<Timer> OnArrive => OnArriveHandler;
        private readonly EventHandler<Timer> OnArriveHandler = new();
        protected override void OnCompleted() { }
        protected override void OnError(Exception error) { }
        protected override void OnNext(ITimeData value) => Update(value.Delta);
        private void Update(float delta)
        {
            if (!IsPlaying.Value) return;
            Delta.Value = delta * Scale.Value;
            Time.SetIfNotEquals(Time.Value + Delta.Value, beforeSet: false);
            OnUpdateHandler.Invoke(this);
            if (IsPlaying.Value && Time.Value >= Target.Value) OnArriveHandler.Invoke(this);
        }
        public void Restart(bool update = true)
        {
            if (Target.Value == 0) Time.SetIfNotEquals(0);
            else
            {
                Time.SetIfNotEquals(Math.Max(0, Time.Value - Target.Value), beforeSet: false);
                while (Time.Value >= Target.Value)
                {
                    if (update) Update(0);
                    Time.SetIfNotEquals(Math.Max(0, Time.Value - Target.Value), beforeSet: false);
                }
            }
            Play();
        }
        public void Start() { Time.SetIfNotEquals(0); Play(); }
        public void Stop() { Pause(); Time.SetIfNotEquals(0); }
        public void Play() => IsPlaying.SetIfNotEquals(true);
        public void Pause() => IsPlaying.SetIfNotEquals(false);

    }
}