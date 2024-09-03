using Main.RXs;
using UnityEngine;

namespace Main.Game
{
    public class BodyMoveable : BodyPartComponent, IMoveable
    {
        public IRXsProperty<Vector3> Direction => AwakeSelf<BodyMoveable>().direction;
        [SerializeField] private RXsProperty_SerializeField<Vector3> direction = new();
        public IRXsProperty<float> Speed => AwakeSelf<BodyMoveable>().speed;
        [SerializeField] private RXsProperty_SerializeField<float> speed = new();
        public IRXsProperty<Vector3> Delta => AwakeSelf<BodyMoveable>().delta;
        private readonly RXsProperty_SerializeField<Vector3> delta = new();
        public IRXsProperty<ITransformHandler> TransformHandler => AwakeSelf<BodyMoveable>().transformHandler;
        private readonly RXsProperty_SerializeField<ITransformHandler> transformHandler = new();
        protected override void OnGameComponentAwake()
        {
            BodyPart.BodyComponents.OfType<ITransformHandler>().FirstOrDefault(transformHandler);
        }
        private void Update() => UpdateMoveable();
        protected virtual void UpdateMoveable() { }
    }
}
