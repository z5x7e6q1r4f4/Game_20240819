
namespace Main.Game
{
    public abstract class BodyPartComponent : GameComponent
    {
        public BodyPart BodyPart => bodyPart ??= GetComponent<BodyPart>();
        private BodyPart bodyPart;
        protected Property<Body> Body => BodyPart.Body;
    }
}