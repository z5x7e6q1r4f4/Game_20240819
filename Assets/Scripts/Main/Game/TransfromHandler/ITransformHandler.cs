using UnityEngine;

namespace Main.Game
{
    public interface ITransformHandler
    {
        Vector3 Position { get; set; }
        Quaternion Rotation { get; set; }
    }
}