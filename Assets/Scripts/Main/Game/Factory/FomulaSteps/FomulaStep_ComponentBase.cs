using Main.RXs;

namespace Main.Game.FomulaSteps
{
    public abstract class FomulaStep_ComponentBase<T> : FomulaStep
    {
        public IRXsCollection_Readonly<T> BodyPartComponents => bodyPartComponents;
        private RXsCollection_SerializeField<T> bodyPartComponents = new();
        private RXsCollection_SerializeField<GameComponent> bodyPartComponentsUntyped = new();
        public IRXsCollection_Readonly<T> BodyComponents => bodyComponents;
        private RXsCollection_SerializeField<T> bodyComponents = new();
        private RXsCollection_SerializeField<GameComponent> bodyComponentsUntyped = new();
        protected override void OnGameComponentAwake()
        {
            Fomula.Factory.ObserverOn(factory => factory.BodyPart.GameComponentList, bodyPartComponentsUntyped);
            bodyPartComponentsUntyped.OfType(bodyPartComponents);
            Fomula.Factory.ObserverOn(factory => factory.BodyPart.BodyComponents, bodyComponentsUntyped);
            bodyComponentsUntyped.OfType(bodyComponents);
        }
    }
}
