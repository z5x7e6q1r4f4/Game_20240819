using Main.RXs;
using UnityEngine;

namespace Main.Game
{
    public interface IMoveable
    {
        IRXsProperty<Vector3> Direction { get; }
        IRXsProperty<float> Speed { get; }
        IRXsProperty<Vector3> Delta { get; }
        IRXsProperty<ITransformHandler> TransformHandler { get; }
    }
}
