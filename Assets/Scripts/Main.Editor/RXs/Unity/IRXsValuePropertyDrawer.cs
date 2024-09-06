using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Main.RXs.Editor
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
                foreach (var child in EditorUtility.GetChildren(property))
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
                var RXsProperty = property.GetParent().GetValue<IObservableProperty>();
                previous = RXsProperty.Value;
                propertyField.RegisterValueChangeCallback(e =>
                {
                    var current = RXsProperty.Value;
                    if (Equals(previous, current)) return;
                    RXsProperty.SetValue(previous, false, false);
                    RXsProperty.SetValue(current);
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