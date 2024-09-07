using System;

namespace Main
{
    public sealed class Timer : ITimeUpdator, IDisposable, IReuseable.IOnRelease
    {
        //Reuse
        Reuse.IPool IReuseable.Pool { get; set; }
        private static Reuse.IPool<Timer> StaticPool
            => staticPool ??= Reuse.GetPool<Timer>(releaseOnClear: () => staticPool = null);
        private static Reuse.IPool<Timer> staticPool;
        public static Timer GetFromReusePool(IDisposable disposable, float target = 0, float time = 0, float scale = 1, bool isPlaying = true)
        {
            var timer = StaticPool.Get(true);
            timer.disposable = disposable;
            timer.Target.Value = target;
            timer.Time.Value = time;
            timer.Scale.Value = scale;
            timer.IsPlaying.Value = isPlaying;
            //
            timer.disposable = Disposable.Create(
                timer.disposable,
                timer.IsPlaying.AfterSet.Immediately().Subscribe(e =>
                {
                    if (e.IsSame || e.Current) return;
                    timer.Delta.SetIfNotEqule(0);
                })
                );
            return timer;
        }
        void IReuseable.IOnRelease.OnRelease()
        {
            OnUpdateHandler.Clear();
            OnArriveHandler.Clear();
            Delta.Value = 0;
            disposable.Dispose();
            disposable = null;
        }
        void IDisposable.Dispose() => this.ReleaseToReusePool();
        //
        public IProperty<float> Time { get; } = new PropertySerializeField<float>();
        public IProperty<float> Delta { get; } = new PropertySerializeField<float>();
        public IProperty<float> Scale { get; } = new PropertySerializeField<float>();
        public IProperty<bool> IsPlaying { get; } = new PropertySerializeField<bool>();
        IObservable<ITimeUpdator> ITimeUpdator.OnUpdate => OnUpdate;
        public IObservable<Timer> OnUpdate => OnUpdateHandler;
        private EventHandler<Timer> OnUpdateHandler { get; } = new();
        public void Update(float delta)
        {
            if (!IsPlaying.Value) return;
            Delta.Value = delta * Scale.Value;
            Time.Value += Delta.Value;
            OnUpdateHandler.Invoke(this);
            if (IsPlaying.Value && Time.Value >= Target.Value) OnArriveHandler.Invoke(this);
        }
        public PropertySerializeField<float> Target = new();
        public IObservable<Timer> OnArrive => OnArriveHandler;
        private readonly EventHandler<Timer> OnArriveHandler = new();
        private IDisposable disposable;
        public void Restart() { Time.Value = MathF.Max(0, Time.Value - Target.Value); Play(); }
        public void Start() { Time.SetIfNotEqule(0); Play(); }
        public void Stop() { Pause(); Time.SetIfNotEqule(0); }
        public void Play() => IsPlaying.SetIfNotEqule(true);
        public void Pause() => IsPlaying.SetIfNotEqule(false);
    }
}