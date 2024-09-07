namespace Main.Game.FomulaSteps
{
    public abstract class FomulaStep_ComponentBase<T> : FomulaStep
    {
        public ICollectionReadonly<T> BodyPartComponents => bodyPartComponents;
        private CollectionSerializeField<T> bodyPartComponents = new();
        private CollectionSerializeField<GameComponent> bodyPartComponentsUntyped = new();
        public ICollectionReadonly<T> BodyComponents => bodyComponents;
        private CollectionSerializeField<T> bodyComponents = new();
        private CollectionSerializeField<GameComponent> bodyComponentsUntyped = new();
        protected override void OnGameComponentAwake()
        {
            Fomula.Factory.ObserverOn(factory => factory.BodyPart.GameComponentList, bodyPartComponentsUntyped);
            bodyPartComponentsUntyped.OfType(bodyPartComponents);
            Fomula.Factory.ObserverOn(factory => factory.BodyPart.BodyComponents, bodyComponentsUntyped);
            bodyComponentsUntyped.OfType(bodyComponents);
        }
    }
}
