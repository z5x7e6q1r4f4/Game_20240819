using Main.RXs;
using System;
using UnityEngine;

namespace Main.Game
{
    public class Body : GameComponent
    {
        [field: SerializeField] public RXsCollection_SerializeField<BodyPart> BodyParts { get; private set; } = new();
        public IRXsCollection_Readonly<GameComponent> BodyComponents => bodyComponents;
        private readonly RXsCollection_SerializeField<GameComponent> bodyComponents = new();
        private DisposableDictonary disposable = new();
        protected override void OnGameComponentAwake()
        {
            BodyParts.LinkItem(this, bodyPart => bodyPart.Body);
            GameComponentList.ConnectTo(bodyComponents);
            BodyParts.AfterAdd.Immediately().Subscribe(AfterBodyPartAdd);
            BodyParts.AfterRemove.Subscribe(AfterBodyPartRemove);
        }
        protected virtual void AfterBodyPartRemove(IRXsCollection_AfterRemove<BodyPart> e)
        {
            disposable.Add(e.Item, e.Item.GameComponentList.ConnectTo(bodyComponents));
        }
        protected virtual void AfterBodyPartAdd(IRXsCollection_AfterAdd<BodyPart> e)
        {
            disposable.Dispose(e.Item);
        }
    }
}