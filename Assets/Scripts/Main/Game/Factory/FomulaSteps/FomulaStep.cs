using Main.RXs;

namespace Main.Game.FomulaSteps
{
    [System.Serializable]
    public abstract class FomulaStep : GameComponent
    {
        public Fomula Fomula => fomula ??= GetComponent<Fomula>();
        private Fomula fomula;
        public IRXsObservable<FomulaStep> OnEnterStep => AwakeSelf<FomulaStep>().onEnterStep;
        public IRXsObservable<FomulaStep> OnExitStep => AwakeSelf<FomulaStep>().onExitStep;
        private readonly RXsEventHandler<FomulaStep> onEnterStep = new();
        private readonly RXsEventHandler<FomulaStep> onExitStep = new();
        public void EnterStep() => onEnterStep.Invoke(this);
        public void ExitStep() => onExitStep.Invoke(this);
    }
}
