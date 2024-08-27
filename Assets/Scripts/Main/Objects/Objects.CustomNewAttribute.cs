using System;
using System.Linq;
using UnityEngine;

namespace Main
{
    public static partial class Objects
    {
        [AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct)]
        public abstract class CustomNewAttribute : Attribute
        {
            public abstract T New<T>(Type type, params object[] args);
        }
        public class ScriptableObjectNewAttribute : CustomNewAttribute
        {
            public readonly static ScriptableObjectNewAttribute Instance = new();
            public override T New<T>(Type type, params object[] args)
            {
                var result = ScriptableObject.CreateInstance(type);
                if (result is T typed) return typed;
                throw new Exception("Type error");
            }
        }
        public class ComponentNewAttribute : CustomNewAttribute
        {
            public readonly static ComponentNewAttribute Instance = new();
            public override T New<T>(Type type, params object[] args)
            {
                var hideFlag = args.OfType<HideFlags>().FirstOrDefault();
                var gameObject = args.OfType<GameObject>().FirstOrDefault();
                gameObject ??= new GameObject(type.Name);
                var result = gameObject.AddComponent(type);
                result.hideFlags = hideFlag;
                if (result is T typed) return typed;
                throw new Exception("Type error");
            }
        }
    }
}
