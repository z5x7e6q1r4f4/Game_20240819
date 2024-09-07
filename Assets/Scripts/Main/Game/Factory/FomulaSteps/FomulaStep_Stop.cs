namespace Main.Game.FomulaSteps
{
    public class FomulaStep_Stop : FomulaStep
    {
        protected override void OnGameComponentEnable()
        {
            base.OnGameComponentEnable();
            OnEnterStep.Subscribe(Fomula.FomulaStop);
        }
    }
}