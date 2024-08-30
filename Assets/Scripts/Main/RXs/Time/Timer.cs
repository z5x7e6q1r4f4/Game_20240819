using System;
using UnityEngine;

namespace Main.RXs
{
    public class Timer :
        RXsObserverItemReusable<Timer, ITimeData>,
        ITimeData,
        ITimeSubject
    {
        //Property
        public float Target { get => target; set => target = Mathf.Max(0, value); }
        private float target;
        public float Time { get; set; }
        public float Delta { get; private set; }
        public float Scale { get => scale; set => scale = Mathf.Max(0, value); }
        private float scale;
        public TimeState State { get; set; }
        //Event
        public IRXsObservable<Timer> OnUpdate => onUpdate;
        private readonly RXsEventHandler<Timer> onUpdate = new();
        public IRXsObservable<Timer> OnArrive => onArrive;
        private readonly RXsEventHandler<Timer> onArrive = new();
        //Observer
        protected override void OnNext(ITimeData value)
        {
            Delta = value.Delta * Scale * (int)State;
            if (State == TimeState.Play)
            {
                Time += Delta;
                onUpdate.Invoke(this);
                if (Time >= Target) onArrive.Invoke(this);
            }
        }
        protected override void OnCompleted() { }
        protected override void OnError(Exception error) { }
        //Observable
        IDisposable IObservable<ITimeData>.Subscribe(IObserver<ITimeData> observer) => onUpdate.Subscribe(observer);
        IDisposable IObservable.Subscribe(IObserver observer) => this.SubscribeToTyped<ITimeData>(observer);
        //Reuse
        public static Timer GetFromReusePool(
            IRXsObservable<ITimeData> timeObservable,
            float targt,
            float time = 0,
            float scale = 1,
            TimeState state = TimeState.Play
            )
        {
            var timer = GetFromReusePool();
            timer.Target = targt;
            timer.Time = time;
            timer.Scale = scale;
            timer.State = state;
            timeObservable.SubscribeToTyped(timer);
            return timer;
        }
        protected override void OnRelease()
        {
            onArrive.Dispose();
            onUpdate.Dispose();
            base.OnRelease();
        }
        //Function
        public void Stop()
        {
            Time = 0;
            Pause();
        }
        public void Start()
        {
            Time = 0;
            Play();
        }
        public void Pause() => State = TimeState.Pause;
        public void Play() => State = TimeState.Play;
        public void Restart()
        {
            Time -= Target;
            Time = Math.Max(Time, 0);
            Play();
        }
    }
}