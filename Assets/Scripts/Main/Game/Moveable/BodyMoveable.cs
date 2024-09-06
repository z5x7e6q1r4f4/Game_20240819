using Main.RXs;
using UnityEngine;

namespace Main.Game
{
    public class BodyMoveable : BodyPartComponent, IMoveable
    {
        public IObservableProperty<Vector3> Direction => AwakeSelf<BodyMoveable>().direction;
        [SerializeField] private ObservableProperty_SerializeField<Vector3> direction = new();
        public IObservableProperty<float> Speed => AwakeSelf<BodyMoveable>().speed;
        [SerializeField] private ObservableProperty_SerializeField<float> speed = new();
        public IObservableProperty<Vector3> Delta => AwakeSelf<BodyMoveable>().delta;
        private readonly ObservableProperty_SerializeField<Vector3> delta = new();
        public IObservableProperty<ITransformHandler> TransformHandler => AwakeSelf<BodyMoveable>().transformHandler;
        private readonly ObservableProperty_SerializeField<ITransformHandler> transformHandler = new();
        protected override void OnGameComponentAwake()
        {
            BodyPart.BodyComponents.OfType<ITransformHandler>().FirstOrDefault(transformHandler);
        }
        private void Update() => UpdateMoveable();
        protected virtual void UpdateMoveable() { }
    }
}
