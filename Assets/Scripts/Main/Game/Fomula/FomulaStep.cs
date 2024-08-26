namespace Main.Game
{
    [System.Serializable]
    public abstract class FomulaStep
    {
        public Fomula Fomula { get; set; }
        public abstract void EnterStep();
        public abstract void ExitStep();
        public abstract class WithComponentBase { }
    }
}
