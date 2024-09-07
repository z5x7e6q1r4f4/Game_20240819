using UnityEngine;

namespace Main.Game
{
    public interface IMoveable
    {
        IProperty<Vector3> Direction { get; }
        IProperty<float> Speed { get; }
        IProperty<Vector3> Delta { get; }
        IProperty<ITransformHandler> TransformHandler { get; }
    }
}
