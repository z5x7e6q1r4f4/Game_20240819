using Main.RXs;
using UnityEngine;

namespace Main.Game
{
    public class BodyPart : GameComponent
    {
        [field: SerializeField] public RXsProperty_SerializeField<Body> Body { get; private set; } = new();
        public IRXsCollection_Readonly<GameComponent> BodyComponents => bodyComponents;
        private RXsCollection_SerializeField<GameComponent> bodyComponents = new();
        protected override void OnGameComponentAwake()
        {
            Body.LinkCollection(this, body => body.BodyParts);
            Body.ObserverOn(body => body.BodyComponents).ConnectTo(bodyComponents);
        }
    }
}