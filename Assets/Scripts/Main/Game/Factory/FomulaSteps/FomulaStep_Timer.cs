using Main.RXs;
using System;
using UnityEngine;

namespace Main.Game.FomulaSteps
{
    public class FomulaStep_Timer : FomulaStep_ComponentBase<TimeNode>
    {
        [field: SerializeField] public RXsProperty_SerializeField<float> Target { get; private set; } = new();
        private RXsProperty_SerializeField<TimeNode> timeNode = new();
        protected IRXsProperty_Readonly<RXsTimer> Timer => timer;
        private readonly RXsProperty_SerializeField<RXsTimer> timer = new();
        protected override void OnGameComponentAwake()
        {
            base.OnGameComponentAwake();
            BodyComponents.FirstOrDefault(timeNode);
            timeNode.AfterSet.Immediately().Subscribe(SetUpTimer);
            OnEnterStep.Subscribe(StartTimer);
        }
        private void SetUpTimer(IRXsProperty_AfterSet<TimeNode> e)
        {
            timer.Value?.ReleaseToReusePool();
            if (e.Current != null)
            {
                timer.Value = e.Current.GetTimer(0, state: TimeState.Pause);
                Target.AfterSet.Immediately().Subscribe(e => timer.Value.Target = e.Current).Until(timer.AfterSet);
                timer.Value.OnArrive.Subscribe(timer => { Fomula.FomulaNext(); timer.Stop(); }).Until(timer.AfterSet);
            }
            else { timer.Value = null; }
        }
        private void StartTimer()
        {
            Timer.AfterSet.Immediately().Subscribe((e) =>
            {
                e.Previous?.Stop();
                e.Current?.Start();
            }).Until(OnExitStep);
        }
    }
}