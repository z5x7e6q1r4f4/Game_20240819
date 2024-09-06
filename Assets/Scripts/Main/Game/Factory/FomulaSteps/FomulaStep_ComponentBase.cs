using Main.RXs;

namespace Main.Game.FomulaSteps
{
    public abstract class FomulaStep_ComponentBase<T> : FomulaStep
    {
        public IObservableCollection_Readonly<T> BodyPartComponents => bodyPartComponents;
        private ObservableCollection_SerializeField<T> bodyPartComponents = new();
        private ObservableCollection_SerializeField<GameComponent> bodyPartComponentsUntyped = new();
        public IObservableCollection_Readonly<T> BodyComponents => bodyComponents;
        private ObservableCollection_SerializeField<T> bodyComponents = new();
        private ObservableCollection_SerializeField<GameComponent> bodyComponentsUntyped = new();
        protected override void OnGameComponentAwake()
        {
            Fomula.Factory.ObserverOn(factory => factory.BodyPart.GameComponentList, bodyPartComponentsUntyped);
            bodyPartComponentsUntyped.OfType(bodyPartComponents);
            Fomula.Factory.ObserverOn(factory => factory.BodyPart.BodyComponents, bodyComponentsUntyped);
            bodyComponentsUntyped.OfType(bodyComponents);
        }
    }
}
