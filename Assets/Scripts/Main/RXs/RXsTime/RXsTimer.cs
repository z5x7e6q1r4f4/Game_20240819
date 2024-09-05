using System;
using UnityEngine;

namespace Main.RXs
{
    public class RXsTimer :
        RXsObserverBaseReusable<RXsTimer, IRXsTimeData>,
        IRXsTimeData,
        IRXsTimeSubject
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
        public IRXsObservable<RXsTimer> OnUpdate => onUpdate;
        private readonly IRXsEventHandler<RXsTimer> onUpdate = new RXsEventHandler<RXsTimer>();
        public IRXsObservable<RXsTimer> OnArrive => onArrive;
        private readonly IRXsEventHandler<RXsTimer> onArrive = new RXsEventHandler<RXsTimer>();
        //Observer
        protected override void OnNext(IRXsTimeData value)
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
        IDisposable IObservable<IRXsTimeData>.Subscribe(IObserver<IRXsTimeData> observer) => this.SubscribeToTyped(observer);
        IRXsDisposable IRXsObservable.Subscribe(IRXsObserver observer) => this.SubscribeToTyped<IRXsTimeData>(observer);
        IRXsDisposable IRXsObservable<IRXsTimeData>.Subscribe(IRXsObserver<IRXsTimeData> observer) => OnUpdate.Subscribe(observer);
        //Reuse
        public static RXsTimer GetFromReusePool(
            IRXsObservable<IRXsTimeData> timeObservable,
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
        protected override void Dispose()
        {
            onArrive.Clear();
            onUpdate.Clear();
            base.Dispose();
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