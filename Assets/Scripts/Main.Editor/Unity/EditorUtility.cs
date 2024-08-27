using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Main
{
    public static class EditorUtility
    {
        public static Type GetType(FieldInfo fieldInfo, out bool isArray)
        {
            isArray = true;
            var type = fieldInfo.FieldType;
            while (type != null)
            {
                if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(List<>))
                    return type.GenericTypeArguments.First();
                if (type.IsArray)
                    return type.GetElementType();
                type = type.BaseType;
            }
            isArray = false;
            return fieldInfo.FieldType;
        }
        public static SerializedProperty GetParent(this SerializedProperty serializedProperty)
        {
            var paths = serializedProperty.propertyPath.Split('.').ToList();
            paths.RemoveAt(paths.Count - 1);
            if (paths.Last() == "Array") paths.RemoveAt(paths.Count - 1);
            string path = null;
            foreach (var p in paths) path += $".{p}";
            path = path.Remove(0, 1);
            return serializedProperty.serializedObject.FindProperty(path);
        }
        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty serializedProperty)
        {
            var child = serializedProperty.Copy();
            if (!child.Next(true)) yield break;
            do { yield return child.Copy(); } while (child.Next(false));
        }
        public static IEnumerable<SerializedProperty> GetChildrenVisible(this SerializedProperty serializedProperty)
        {
            var child = serializedProperty.Copy();
            if (!child.NextVisible(true)) yield break;
            do { yield return child.Copy(); } while (child.NextVisible(false));
        }
        public static object GetValue(this SerializedProperty serializedProperty)
        {
            object current = serializedProperty.serializedObject.targetObject;
            var paths = serializedProperty.propertyPath.Split('.').ToList();
            while (paths.Count > 0)
            {
                var path = paths.First();
                var type = current.GetType();
                switch (path)
                {
                    default:
                        var field = type.GetField(path, BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                        current = field.GetValue(current);
                        break;
                }
                paths.RemoveAt(0);
            }
            return current;
        }
    }
}