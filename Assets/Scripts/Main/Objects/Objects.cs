using System;
using System.Reflection;
using UnityEngine;

namespace Main
{
    public static partial class Objects
    {
        public static T New<T>(
            Type type = null,
            CustomNewAttribute customNew = null,
            params object[] args)
        {
            type ??= typeof(T);
            customNew ??= type.GetCustomAttribute<CustomNewAttribute>(true) ?? GetDefaultNew(type);
            if (customNew != null) return customNew.New<T>(type, args);
            else return (T)Activator.CreateInstance(type, args: args);
        }
        public static T Clone<T>(
            T item,
            CustomCloneAttribute customClone = null,
            params object[] args)
        {
            var type = item.GetType();
            customClone ??= type.GetCustomAttribute<CustomCloneAttribute>(true) ?? GetDefaultClone(type);
            if (customClone != null) return customClone.Clone(item, args);
            if (item is ICloneable cloneable) return (T)cloneable.Clone();
            throw new InvalidOperationException();
        }
    }
}
