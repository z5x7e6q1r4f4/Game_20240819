namespace Main.Game.FomulaSteps
{
    [System.Serializable]
    public abstract class FomulaStep : GameComponent
    {
        public Fomula Fomula => fomula ??= GetComponent<Fomula>();
        private Fomula fomula;
        public abstract void EnterStep();
        public abstract void ExitStep();
    }
}
