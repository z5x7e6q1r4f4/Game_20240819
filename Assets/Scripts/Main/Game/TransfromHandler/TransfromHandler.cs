using System;
using UnityEngine;

namespace Main.Game
{
    public enum TransformType
    {
        Transform,
        Rigidbody,
        Rigidbody2D,
    }
    public class TransfromHandler : GameComponent, ITransformHandler
    {
        public TransformType type;
        public Vector3 Position { get => AwakeSelf<TransfromHandler>().getPosition(); set => AwakeSelf<TransfromHandler>().setPosition(value); }
        public Quaternion Rotation { get => AwakeSelf<TransfromHandler>().getRotation(); set => AwakeSelf<TransfromHandler>().setRotation(value); }
        //
        protected Rigidbody2D Rigidbody2D => rigidbody2D = rigidbody2D != null ? rigidbody2D : GetComponent<Rigidbody2D>();
        private new Rigidbody2D rigidbody2D;
        //
        protected Rigidbody Rigidbody => rigidbody = rigidbody != null ? rigidbody : GetComponent<Rigidbody>();
        private new Rigidbody rigidbody;
        //
        Func<Vector3> getPosition;
        Func<Quaternion> getRotation;
        Action<Vector3> setPosition;
        Action<Quaternion> setRotation;
        protected override void OnGameComponentAwake()
        {
            switch (type)
            {
                case TransformType.Transform:
                    getPosition = () => transform.localPosition;
                    setPosition = value => transform.localPosition = value;
                    getRotation = () => transform.localRotation;
                    setRotation = value => transform.localRotation = value;
                    break;
                case TransformType.Rigidbody:
                    getPosition = () => Rigidbody.position;
                    setPosition = value => Rigidbody.MovePosition(value);
                    getRotation = () => Rigidbody.rotation;
                    setRotation = value => Rigidbody.MoveRotation(value);
                    break;
                case TransformType.Rigidbody2D:
                    getPosition = () => Rigidbody2D.position;
                    setPosition = value => Rigidbody2D.MovePosition(value);
                    getRotation = () => Quaternion.AngleAxis(Rigidbody2D.rotation, Vector3.forward);
                    setRotation = value => Rigidbody2D.MoveRotation(value);
                    break;
            }
        }
    }
}