using Main.RXs;
using UnityEngine;

namespace Main.Game
{
    public class BodyPart : GameComponent
    {
        [field: SerializeField] public ObservableProperty_SerializeField<Body> Body { get; private set; } = new();
        //Component
        public IObservableCollection_Readonly<GameComponent> BodyComponents => bodyComponents;
        private ObservableCollection_SerializeField<GameComponent> bodyComponents = new();
        //Time
        //public IObservableProperty_Readonly<TimeNode> TimeNode => timeNode ??= Body.Select(body => body?.TimeNode);
        //private IObservableProperty_Readonly<TimeNode> timeNode ;
        protected override void OnGameComponentAwake()
        {
            Body.LinkCollection(this, body => body.BodyParts);
            Body.ObserverOn(body => body.BodyComponents).ConnectTo(bodyComponents);
        }
    }
}