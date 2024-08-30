using System;
using System.Collections;
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
        public static GameObject GetGameObject(this SerializedProperty serializedProperty)
            => (serializedProperty.serializedObject.targetObject as Component)?.gameObject;
        public static Type GetPropertyType(this SerializedProperty serializedProperty)
        {
            serializedProperty.GetValueCore(out var obj, out var fieldInfo, out var array, out var _);
            if (obj != null) return fieldInfo.FieldType;
            if (array != null) return TypeUtility.GetArrayType(array);
            throw new Exception();
        }
        public static int GetIndex(this SerializedProperty serializedProperty)
        {
            serializedProperty.GetValueCore(out _, out _, out _, out var index);
            return index;
        }
        public static bool IsArrayElement(this SerializedProperty serializedProperty) => serializedProperty.GetParent().isArray;
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
        public static IEnumerable<SerializedProperty> GetChildren(this SerializedProperty serializedProperty, bool visible = true)
        {
            serializedProperty = serializedProperty.Copy();
            var depth = serializedProperty.depth + 1;
            Func<bool, bool> moveNext = visible ? serializedProperty.NextVisible : serializedProperty.Next;
            if (!moveNext(true) || serializedProperty.depth != depth) yield break;
            do { yield return serializedProperty.Copy(); }
            while (moveNext(false) && serializedProperty.depth == depth);
        }
        public static T GetValue<T>(this SerializedProperty serializedProperty)
        {
            object result = serializedProperty.propertyType switch
            {
                SerializedPropertyType.ObjectReference => serializedProperty.objectReferenceValue,
                SerializedPropertyType.ManagedReference => serializedProperty.managedReferenceValue,
                _ => serializedProperty.GetValueByPath(),
            };
            if (result == null || result is T) return (T)result;
            else return default;
        }
        public static void SetValue(this SerializedProperty serializedProperty, object value)
        {
            switch (serializedProperty.propertyType)
            {
                case SerializedPropertyType.ObjectReference:
                    serializedProperty.objectReferenceValue = (UnityEngine.Object)value; break;
                case SerializedPropertyType.ManagedReference:
                    serializedProperty.managedReferenceValue = value; break;
                default:
                    serializedProperty.SetValueByPath(value); break;
            }
        }
        private static void GetValueCore(this SerializedProperty serializedProperty, out object obj, out FieldInfo fieldInfo, out IList array, out int index)
        {
            obj = serializedProperty.serializedObject.targetObject;
            var paths = serializedProperty.propertyPath.Split('.').ToList();
            var last = paths.Count - 1;
            for (int i = 0; i < paths.Count; i++)
            {
                var type = obj.GetType();
                var path = paths[i];
                switch (path)
                {
                    default:
                        fieldInfo = type.GetFieldInherit(path, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
                        if (i != last) obj = fieldInfo.GetValue(obj);
                        else { array = null; index = -1; return; }
                        break;
                    case "Array":
                        var indexPath = paths[i + 1]; i++;
                        index = int.Parse(indexPath.Remove(indexPath.Length - 1, 1).Remove(0, 5));//Data[x]=>x
                        array = obj as IList;
                        if (i != last) obj = array[index];
                        else { obj = null; fieldInfo = null; return; }
                        break;
                }
            }
            throw new Exception();
        }
        private static object GetValueByPath(this SerializedProperty serializedProperty)
        {
            serializedProperty.GetValueCore(out var obj, out var fieldInfo, out var array, out var index);
            if (obj != null) return fieldInfo.GetValue(obj);
            if (array != null) return array[index];
            throw new Exception();
        }
        private static void SetValueByPath(this SerializedProperty serializedProperty, object value)
        {
            serializedProperty.GetValueCore(out var obj, out var fieldInfo, out var array, out var index);
            if (obj != null) { fieldInfo.SetValue(obj, value); return; }
            if (array != null) { array[index] = value; return; }
            throw new Exception();
        }
    }
}