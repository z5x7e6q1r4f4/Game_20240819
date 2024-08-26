using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Main
{
    public static class TypeUtility
    {
        public static IEnumerable<Type> GetSubClass(Type type)
        {
            foreach (var assembly in AssemblyUtility.AllAssemblies)
            {
                foreach (var t in assembly.GetTypes())
                {
                    if (type.IsAssignableFrom(t)) yield return t;
                }
            }
        }
        public static IEnumerable<Type> GetSubClassInstanceable(Type type)
            => GetSubClass(type).Where(static t => !t.IsAbstract && !t.IsInterface);
    }
}
