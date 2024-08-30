namespace Main.Game
{
    public class BodyPartComponent : GameComponent
    {
        public BodyPart BodyPart => bodyPart ??= GetComponent<BodyPart>();
        private BodyPart bodyPart;
    }
}