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
        [Default(typeof(ScriptableObject))]
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
        [Default(typeof(Component))]
        public class ComponentNewAttribute : CustomNewAttribute
        {
            public string name = null;
            public HideFlags hideFlags = default;
            public bool tryFind = false;
            public readonly static ComponentNewAttribute Instance = new();
            public override T New<T>(Type type, params object[] args)
            {
                //HideFlag
                var hideFlagsArg = args.OfType<HideFlags>();
                if (hideFlagsArg.Count() > 0) hideFlags = hideFlagsArg.First();
                //GameObject
                var gameObject = args.OfType<GameObject>().FirstOrDefault();
                if (gameObject is null && name != null && tryFind) gameObject = GameObject.Find(name);
                gameObject ??= new GameObject(name ?? type.Name);
                //Main
                var result = gameObject.AddComponent(type);
                result.hideFlags = hideFlags;
                if (result is T typed) return typed;
                throw new Exception("Type error");
            }
        }
    }
}
