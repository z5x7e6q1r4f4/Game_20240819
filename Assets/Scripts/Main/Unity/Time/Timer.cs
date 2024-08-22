using System;

namespace Main.RXs
{
    public class Timer : Reuse.ObjectBase<Timer>, ITimeData, ITimeObservable, ITimeObserver
    {
        //public TimeNode TimeNode { get; private set; }
        //public float Target { get; set; }
        public float Time { get; set; }
        public float Delta { get; private set; }
        //public float Scale { get; set; } = 1;
        //public TimeState State { get; set; }
        //Event
        IObservable<ITimeData> ITimeObservable.OnNext => onNext;
        public IObservable<Timer> OnNext => onNext;
        private readonly RXsEventHandler<Timer> onNext = new();
        public IObservable<Timer> OnCompleted => onCompleted;
        private readonly RXsEventHandler<Timer> onCompleted = new();
        //void System.IObserver<TimeNode>.OnNext(TimeNode timeNode)
        //{
        //    Delta = timeNode.DeltaTime * Scale * (int)TimeState.Play;
        //    if (State == TimeState.Stop) return;
        //    if (Current < Target)
        //    {
        //        Current += Delta;
        //    }
        //    OnNext?.Invoke(this);
        //}
        //void System.IObserver<TimeNode>.OnCompleted() => OnCompleted?.Invoke(this);
        //void System.IObserver<TimeNode>.OnError(Exception error) { }
        //void IObserver.OnNext(object value) => this.OnNextToTyped<TimeNode>(value);
        //void IObserver.OnCompleted() => this.OnCompletedToTyped<TimeNode>();
        //void IObserver.OnError(Exception error) => this.OnErrorToTyped<TimeNode>(error);
        //public void Reset() { }
        //public void Restart() { }
        //public static Timer GetFromReusePool(
        //    TimeNode timeNode,
        //    float targt,
        //    float current = 0,
        //    float scale = 1,
        //    TimeState state = TimeState.Play,
        //    Action<Timer> onNext = null,
        //    Action<Timer> onCompleted = null
        //    )
        //{
        //}
    }
}