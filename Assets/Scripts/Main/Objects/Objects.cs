using System;
using System.Reflection;
using UnityEngine;

namespace Main
{
    public static partial class Objects
    {
        public static T New<T>(
            Type type = null,
            CustomNewAttribute customNewOverride = null,
            CustomNewAttribute customNewFallback = null,
            params object[] args)
        {
            type ??= typeof(T);
            customNewFallback ??= type.GetCustomAttribute<CustomNewAttribute>(true);
            customNewOverride ??= customNewFallback;
            if (customNewOverride != null) return customNewOverride.New<T>(type, args);
            else return (T)Activator.CreateInstance(type ?? typeof(T), args: args);
        }
        public static T Clone<T>(
            T item,
            CustomCloneAttribute customCloneOverride = null,
            CustomCloneAttribute customCloneFallback = null,
            params object[] args)
        {
            var type = item.GetType();
            customCloneFallback ??= type.GetCustomAttribute<CustomCloneAttribute>(true);
            customCloneOverride ??= customCloneFallback;
            if (customCloneOverride != null) return customCloneOverride.Clone(item, args);
            if (item is ICloneable cloneable) return (T)cloneable.Clone();
            throw new InvalidOperationException();
        }
    }
}
