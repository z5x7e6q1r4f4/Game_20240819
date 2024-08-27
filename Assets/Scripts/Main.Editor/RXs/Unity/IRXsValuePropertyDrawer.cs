using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Main.RXs.Unity
{
    [CustomPropertyDrawer(typeof(IRXsValueInspector), true)]
    public class IRXsValuePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
            => new IRXsValueElement(property, fieldInfo);
        private class IRXsValueElement : VisualElement
        {
            public IRXsValueElement(SerializedProperty property, FieldInfo fieldInfo)
            {
                var type = fieldInfo.FieldType;
                foreach (var child in EditorUtility.GetChildrenVisible(property))
                {
                    FieldInfo curFieldInfo = type.GetField(
                        child.name,
                        BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    //
                    if (curFieldInfo == null) continue;
                    if (curFieldInfo.IsDefined(typeof(SerializedPropertyAttribute)))
                    {
                        Add(new SerializedPropertyElement(property.displayName, child, curFieldInfo));
                        continue;
                    }
                    if (curFieldInfo.IsDefined(typeof(SerializedCollectionAttribute)))
                    {
                        Add(new SerializedCollectionElement(property.displayName, child, curFieldInfo));
                        continue;
                    }
                    Add(new PropertyField(child));
                }
            }
        }
        private class SerializedPropertyElement : Box
        {
            private object previous;
            public SerializedPropertyElement(string displayName, SerializedProperty property, FieldInfo fieldInfo)
            {
                var propertyField = new PropertyField(property, displayName);
                var RXsProperty = EditorUtility.GetValue(EditorUtility.GetParent(property));
                var propertyInfo = RXsProperty.GetType().GetProperty("Value");
                previous = EditorUtility.GetValue(property);
                propertyField.RegisterValueChangeCallback(e =>
                {
                    var current = EditorUtility.GetValue(e.changedProperty);
                    if (Equals(previous, current)) return;
                    fieldInfo.SetValue(RXsProperty, previous);
                    propertyInfo.SetValue(RXsProperty, current);
                    Debug.Log($"Change from {previous} to {current}");
                    previous = current;
                });
                Add(propertyField);
            }
        }
        private class SerializedCollectionElement : Box
        {
            public SerializedCollectionElement(string displayName, SerializedProperty property, FieldInfo fieldInfo)
            {
                Add(new PropertyField(property, displayName));
            }
        }
    }
}