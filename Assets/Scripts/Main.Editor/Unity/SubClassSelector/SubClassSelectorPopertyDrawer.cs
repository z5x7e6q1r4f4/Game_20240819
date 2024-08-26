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
            private readonly Type type;
            private readonly List<Type> types;
            PopupField<Type> popupField;
            PropertyField propertyField;
            public SubClassSelectorElement(
                SerializedProperty property,
                FieldInfo fieldInfo,
                SubClassSelectorAttribute attribute)
            {
                type = attribute.type ?? EditorUtility.GetType(fieldInfo);
                types = TypeUtility.GetSubClassInstanceable(type).ToList();
                types.Insert(0, null);
                //
                var index = types.IndexOf(property.managedReferenceValue?.GetType());
                popupField = new(types, index, Format, Format);
                popupField.RegisterValueChangedCallback(e =>
                {
                    property.managedReferenceValue = Objects.New<object>(e.newValue);
                    property.serializedObject.ApplyModifiedProperties();
                });
                Add(popupField);
                //
                propertyField = new(property);
                Add(propertyField);
            }
            private string Format(Type type) => $"{type?.Name ?? "Null"}{(type != null ? $"({type.Namespace})" : string.Empty)}";
        }
    }
}
