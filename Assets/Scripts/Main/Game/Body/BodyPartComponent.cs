namespace Main.Game
{
    public abstract class BodyPartComponent : GameComponent
    {
        public BodyPart BodyPart => bodyPart ??= GetComponent<BodyPart>();
        private BodyPart bodyPart;
    }
}