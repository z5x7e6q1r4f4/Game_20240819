using Main.RXs;

namespace Main.Game
{
    public abstract class BodyPartComponent : GameComponent
    {
        public BodyPart BodyPart => bodyPart ??= GetComponent<BodyPart>();
        private BodyPart bodyPart;
        protected ObservablePropertyBase<Body> Body => BodyPart.Body;
    }
}