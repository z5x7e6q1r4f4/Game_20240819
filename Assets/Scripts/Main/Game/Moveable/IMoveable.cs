using Main.RXs;
using UnityEngine;

namespace Main.Game
{
    public interface IMoveable
    {
        IObservableProperty<Vector3> Direction { get; }
        IObservableProperty<float> Speed { get; }
        IObservableProperty<Vector3> Delta { get; }
        IObservableProperty<ITransformHandler> TransformHandler { get; }
    }
}
