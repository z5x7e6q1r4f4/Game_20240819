using Main.RXs;
using UnityEngine;

namespace Main.Game
{
    public class Body : GameComponent
    {
        [field: SerializeField] public RXsCollection_SerializeField<BodyPart> BodyParts { get; } = new();
        protected override void OnGameComponentAwake()
        {
            BodyParts.LinkItem(this, bodyPart => bodyPart.Body);
        }
    }

    public class BodyPart : GameComponent
    {
        [field: SerializeField] public RXsProperty_SerializeField<Body> Body { get; } = new();
        protected override void OnGameComponentAwake()
        {
            Body.LinkCollection(this, body => body.BodyParts);
        }
    }
}