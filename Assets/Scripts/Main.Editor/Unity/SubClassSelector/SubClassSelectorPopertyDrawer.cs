using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Main
{
    [CustomPropertyDrawer(typeof(SubClassSelectorAttribute))]
    public class SubClassSelectorPopertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
            => new SubClassSelectorElement(property, fieldInfo, attribute as SubClassSelectorAttribute);
        private class SubClassSelectorElement : VisualElement
        {
            private enum TargetType
            {
                Normal,
                ScriptableObject,
                Component
            }
            private readonly Type type;
            private readonly List<Type> types;
            private PopupField<Type> popupField;
            private VisualElement propertyField;
            public SubClassSelectorElement(
                SerializedProperty property,
                FieldInfo fieldInfo,
                SubClassSelectorAttribute attribute)
            {
                type = attribute.type ?? EditorUtility.GetType(fieldInfo);
                types = TypeUtility.GetSubClassInstanceable(type).ToList();
                types.Insert(0, null);
                //TargetType
                TargetType targetType = TargetType.Normal;
                if (typeof(UnityEngine.Object).IsAssignableFrom(type)) targetType = TargetType.ScriptableObject;
                if (typeof(UnityEngine.Component).IsAssignableFrom(type)) targetType = TargetType.Component;
                //CurrentValue
                object currentValue = targetType switch
                {
                    TargetType.Normal => property.managedReferenceValue,
                    TargetType.ScriptableObject => property.objectReferenceValue,
                    TargetType.Component => property.objectReferenceValue,
                    _ => throw new Exception(),
                };
                //
                var index = types.IndexOf(currentValue?.GetType());
                popupField = new(types, index, Format, Format);
                //
                popupField.RegisterValueChangedCallback(e =>
                {
                    RemovePreviousValue(property);
                    AddCurrentValue(targetType, property, e.newValue);
                    property.serializedObject.ApplyModifiedProperties();
                    UpdatePropertyField(targetType, property);
                });
                Add(popupField);
                //
                UpdatePropertyField(targetType, property);
            }
            private void RemovePreviousValue(SerializedProperty property)
            {
                if (property.objectReferenceValue == null) return;
                UnityEngine.Object.DestroyImmediate(property.objectReferenceValue);
                property.objectReferenceValue = null;
            }
            private void AddCurrentValue(TargetType targetType, SerializedProperty property, Type type)
            {
                if (type == null) return;
                switch (targetType)
                {
                    case TargetType.Normal:
                        property.managedReferenceValue = Objects.New<object>(type);
                        break;
                    case TargetType.ScriptableObject:
                        property.objectReferenceValue =
                            Objects.New<UnityEngine.Object>(
                                type,
                                customNewFallback: Objects.ScriptableObjectNewAttribute.Instance);
                        break;
                    case TargetType.Component:
                        var gameObject = (property.serializedObject.targetObject as UnityEngine.Component)?.gameObject;
                        property.objectReferenceValue =
                            Objects.New<UnityEngine.Object>(
                                type,
                                customNewFallback: Objects.ComponentNewAttribute.Instance,
                                args: gameObject);
                        break;
                }
            }
            private void UpdatePropertyField(TargetType targetType, SerializedProperty property)
            {
                if (propertyField != null) Remove(propertyField);
                propertyField = targetType switch
                {
                    TargetType.Normal => new PropertyField(property),
                    TargetType.ScriptableObject =>
                        property.objectReferenceValue != null ?
                            new InspectorElement(property.objectReferenceValue) :
                            null,
                    TargetType.Component =>
                        property.objectReferenceValue != null ?
                            new InspectorElement(property.objectReferenceValue) :
                            null,
                    _ => throw new Exception(),
                };
                if (propertyField != null) Add(propertyField);
            }
            private string Format(Type type) => $"{type?.Name ?? "Null"}{(type != null ? $"({type.Namespace})" : string.Empty)}";
        }
    }
}
