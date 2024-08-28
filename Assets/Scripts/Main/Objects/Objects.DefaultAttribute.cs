using System;
using System.Collections.Generic;
using System.Reflection;

namespace Main
{
    public static partial class Objects
    {
        [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
        public class DefaultAttribute : Attribute
        {
            public Type type;
            public DefaultAttribute(Type type) => this.type = type;
        }
        private static Dictionary<Type, CustomCloneAttribute> DefaultCloneAttribute;
        private static Dictionary<Type, CustomNewAttribute> DefaultNewAttribute;
        private static CustomNewAttribute GetDefaultNew(Type type)
        {
            if (DefaultNewAttribute == null) SetUpDefaultNewAttribute();
            while (type != null)
            {
                if (DefaultNewAttribute.TryGetValue(type, out var result)) return result;
                type = type.BaseType;
            }
            return null;
        }
        private static void SetUpDefaultNewAttribute()
        {
            DefaultNewAttribute = new();
            foreach (var type in TypeUtility.GetSubClassInstanceable(typeof(CustomNewAttribute)))
            {
                var att = type.GetCustomAttribute<DefaultAttribute>();
                if (att != null) { DefaultNewAttribute.TryAdd(att.type, Activator.CreateInstance(type) as CustomNewAttribute); }
            }
        }
        private static CustomCloneAttribute GetDefaultClone(Type type)
        {
            if (DefaultCloneAttribute == null) SetUpDefaultCloneAttribute();
            while (type != null)
            {
                if (DefaultCloneAttribute.TryGetValue(type, out var result)) return result;
                type = type.BaseType;
            }
            return null;
        }
        private static void SetUpDefaultCloneAttribute()
        {
            DefaultCloneAttribute = new();
            foreach (var type in TypeUtility.GetSubClassInstanceable(typeof(CustomCloneAttribute)))
            {
                var att = type.GetCustomAttribute<DefaultAttribute>();
                if (att != null) { DefaultCloneAttribute.TryAdd(att.type, Activator.CreateInstance(type) as CustomCloneAttribute); }
            }
        }
    }
}
