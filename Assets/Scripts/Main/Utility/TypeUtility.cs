using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main
{
    public static class TypeUtility
    {
        private static readonly Dictionary<Type, List<Type>> Cache = new();
        public static IEnumerable<Type> GetSubClass(Type type)
        {
            if (!Cache.TryGetValue(type, out var result))
            {
                result = new List<Type>();
                foreach (var assembly in AssemblyUtility.AllAssemblies)
                {
                    foreach (var t in assembly.GetTypes())
                    {
                        if (type.IsAssignableFrom(t)) result.Add(t);
                    }
                }
                Cache.Add(type, result);
            }
            return result;
        }
        public static IEnumerable<Type> GetSubClassInstanceable(Type type)
            => GetSubClass(type).Where(static t => !t.IsAbstract && !t.IsInterface);
    }
}
