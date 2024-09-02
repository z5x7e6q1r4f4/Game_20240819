using Main.RXs;
using UnityEngine;

namespace Main.Game.FomulaSteps
{
    public class FomulaStep_Timer : FomulaStep_ComponentBase<TimeNode>
    {
        [field: SerializeField] public RXsProperty_SerializeField<float> Target { get; private set; } = new();
        private RXsProperty_SerializeField<TimeNode> timeNode = new();
        protected IRXsProperty_Readonly<RXsTimer> Timer => timer;
        private readonly RXsProperty_SerializeField<RXsTimer> timer = new();
        private RXsSubscriptionList subscription = new();
        protected override void OnGameComponentAwake()
        {
            base.OnGameComponentAwake();
            BodyComponents.FirstOrDefault(timeNode);
            RXsSubscriptionList timerSubscription = new();
            timeNode.AfterSet.Immediately().Subscribe(e =>
            {
                RXsTimer t = timer.Value;
                if (t != null)
                {
                    t.ReleaseToReusePool();
                    timerSubscription.Dispose();
                }
                if (e.Current != null)
                {
                    t = e.Current.GetTimer(0, state: TimeState.Pause);
                    timerSubscription.Add(Target.AfterSet.Immediately().Subscribe(e => t.Target = e.Current));
                    timerSubscription.Add(t.OnArrive.Subscribe(timer =>
                    {
                        Fomula.FomulaNext();
                        timer.Stop();
                    }));
                    timer.Value = t;
                }
                else { timer.Value = null; }
            });
        }
        public override void EnterStep()
        {
            subscription.Add(Timer.AfterSet.Immediately().Subscribe(e =>
            {
                e.Previous?.Stop();
                e.Current?.Start();
            }));
        }
        public override void ExitStep() => subscription.Dispose();
    }
}