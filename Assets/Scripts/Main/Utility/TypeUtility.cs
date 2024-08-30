using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public static Type GetArrayType(IList array)
        {
            var type = array.GetType();
            if (type.IsArray) return type.GetElementType();
            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                return type.GenericTypeArguments.First();
            throw new Exception();
        }
        public static FieldInfo GetFieldInherit(this Type type, string name, BindingFlags bindingFlags = BindingFlags.Default)
        {
            while (type != null)
            {
                var fieldInfo = type.GetField(name, bindingFlags);
                if (fieldInfo != null) return fieldInfo;
                type = type.BaseType;
            }
            return null;
        }
        // <Name>k__BackingField
        public static string Name(this FieldInfo fieldInfo, bool withoutBackingField = true)
        {
            var result = fieldInfo.Name;
            if (withoutBackingField) result = result.Replace("<", string.Empty).Replace(">k__BackingField", string.Empty);
            return result;
        }
    }
}
