using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;

namespace Main
{
    public static class EditorUtility
    {
        public static Type GetType(FieldInfo fieldInfo)
        {
            var type = fieldInfo.FieldType;
            while (type != null)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                    return type.GenericTypeArguments.First();
                if (type.IsArray)
                    return type.GetElementType();
                type = type.BaseType;
            }
            return fieldInfo.FieldType;
        }
        public static SerializedProperty GetParent(SerializedProperty serializedProperty)
        {
            var first = serializedProperty.serializedObject.GetIterator();
            return GetParentInternal(first, serializedProperty);
        }
        private static SerializedProperty GetParentInternal(SerializedProperty from, SerializedProperty target)
        {
            var parent = from.Copy();
            var child = from.Copy();
            if (!child.Next(true)) return null;
            do
            {
                if (SerializedProperty.EqualContents(child, target)) return parent;
                var searchChild = GetParentInternal(child, target);
                if (searchChild != null) return searchChild;
            } while (parent.Next(false));
            return null;
        }
    }
}