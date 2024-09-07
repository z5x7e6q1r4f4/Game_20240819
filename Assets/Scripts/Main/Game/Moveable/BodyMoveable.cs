using UnityEngine;

namespace Main.Game
{
    public class BodyMoveable : BodyPartComponent, IMoveable
    {
        public IProperty<Vector3> Direction => AwakeSelf<BodyMoveable>().direction;
        [SerializeField] private PropertySerializeField<Vector3> direction = new();
        public IProperty<float> Speed => AwakeSelf<BodyMoveable>().speed;
        [SerializeField] private PropertySerializeField<float> speed = new();
        public IProperty<Vector3> Delta => AwakeSelf<BodyMoveable>().delta;
        private readonly PropertySerializeField<Vector3> delta = new();
        public IProperty<ITransformHandler> TransformHandler => AwakeSelf<BodyMoveable>().transformHandler;
        private readonly PropertySerializeField<ITransformHandler> transformHandler = new();
        protected override void OnGameComponentAwake()
        {
            BodyPart.BodyComponents.OfType<ITransformHandler>().FirstOrDefault(transformHandler);
        }
        private void Update() => UpdateMoveable();
        protected virtual void UpdateMoveable() { }
    }
}
