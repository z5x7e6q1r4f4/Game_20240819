using System;
using UnityEngine;

namespace Main.Game
{
    public class Body : GameComponent
    {
        [field: SerializeField] public CollectionSerializeField<BodyPart> BodyParts { get; private set; } = new();
        //Component
        public ICollectionReadonly<GameComponent> BodyComponents => bodyComponents;
        private readonly CollectionSerializeField<GameComponent> bodyComponents = new();
        //TimeNode
        //public TimeNode TimeNode => timeNode = timeNode != null ? timeNode : GetComponent<TimeNode>();
        //private TimeNode timeNode;
        protected override void OnGameComponentAwake()
        {
            BodyParts.LinkItem(this, bodyPart => bodyPart.Body);
            GameComponentList.ConnectTo(bodyComponents);
            BodyParts.AfterAdd.Immediately().Subscribe(
                e =>
                e.Item.GameComponentList.
                ConnectTo(bodyComponents).
                Until(
                    BodyParts.AfterRemove.
                    Where(_ => _.Item == e.Item)
                    )
                );
        }
    }
}