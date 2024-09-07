using Main.RXs;
using System;

namespace Main.Game.FomulaSteps
{
    [System.Serializable]
    public abstract class FomulaStep : GameComponent
    {
        public Fomula Fomula => fomula ??= GetComponent<Fomula>();
        private Fomula fomula;
        public IObservable<FomulaStep> OnEnterStep => AwakeSelf<FomulaStep>().onEnterStep;
        public IObservable<FomulaStep> OnExitStep => AwakeSelf<FomulaStep>().onExitStep;
        private readonly EventHandler<FomulaStep> onEnterStep = new();
        private readonly EventHandler<FomulaStep> onExitStep = new();
        public void EnterStep() => onEnterStep.Invoke(this);
        public void ExitStep() => onExitStep.Invoke(this);
    }
}
