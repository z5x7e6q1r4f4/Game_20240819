using System;
using Main.RXs;

namespace Main
{
    partial class TimeAndUpdate
    {
        public sealed class Timer :
            TimeUpdator,
            IDisposable,
            IReuseable.IOnRelease
        {
            Reuse.IPool IReuseable.Pool { get; set; }
            private static Reuse.IPool<Timer> StaticPool
                => staticPool ??= Reuse.GetPool<Timer>(releaseOnClear: () => staticPool = null);
            private static Reuse.IPool<Timer> staticPool;
            public ObservableProperty_SerializeField<float> Target = new();
            public IObservable<Timer> OnArrive => onArrive;
            private readonly ObservableEventHandler<Timer> onArrive = new();
            private IDisposable disposable;
            public void Start() { Time.SetIfNotEqule(0); Play(); }
            public void Stop() { Pause(); Time.SetIfNotEqule(0); }
            public void Play() => IsPlaying.SetIfNotEqule(true);
            public void Pause() => IsPlaying.SetIfNotEqule(false);
            public override void Update(float delta)
            {
                base.Update(delta);
                if (IsPlaying.Value && Time.Value >= Target.Value) onArrive.Invoke(this);
            }
            public static Timer GetFromReusePool(IDisposable disposable, float target = 0, float time = 0, float scale = 1, bool isPlaying = true)
            {
                var timer = StaticPool.Get(false);
                timer.disposable = disposable;
                timer.Target.Value = target;
                timer.Time.Value = time;
                timer.Scale.Value = scale;
                timer.IsPlaying.Value = isPlaying;
                return timer;
            }
            void IReuseable.IOnRelease.OnRelease()
            {
                OnUpdateHandler.Clear();
                onArrive.Clear();
                Delta.Value = 0;
                disposable.Dispose();
                disposable = null;
            }
            void IDisposable.Dispose() => this.ReleaseToReusePool();
        }
    }
}