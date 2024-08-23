using System;

namespace Main.RXs
{
    public class Timer :
        ObserverNodeReusable<ITimeData, Timer>,
        ITimeData,
        ITimeSubject
    {
        //Property
        public float Target { get; set; }
        public float Time { get; set; }
        public float Delta { get; private set; }
        public float Scale { get; set; } = 1;
        public TimeState State { get; set; }
        //Event
        public IObservable<Timer> OnUpdate => onUpdate;
        private readonly RXsEventHandler<Timer> onUpdate = new();
        public IObservable<Timer> OnArrive => onArrive;
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
            base.OnNext(value);
        }
        //Observable
        IDisposable System.IObservable<ITimeData>.Subscribe(System.IObserver<ITimeData> observer) => onUpdate.Subscribe(observer);
        IDisposable IObservable.Subscribe(IObserver observer) => this.SubscribeToTyped<ITimeData>(observer);
        //Reuse
        public static Timer GetFromReusePool(
            ITimeObservable timeObservable,
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
            timeObservable.SubscribeToTyped<ITimeData>(timer);
            return timer;
        }
    }
}