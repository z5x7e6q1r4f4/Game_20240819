using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
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
            protected readonly SerializedProperty property;
            protected readonly Type type;
            protected readonly bool isArray;
            protected readonly IReadOnlyCollection<Type> types;
            protected readonly PopupField<Type> popupField;
            protected VisualElement propertyField;
            public SubClassSelectorElement(
                SerializedProperty property,
                FieldInfo fieldInfo,
                SubClassSelectorAttribute attribute)
            {
                this.property = property.Copy();
                //
                type = EditorUtility.GetType(fieldInfo, out isArray);
                type = attribute.type ?? type;
                //Types
                var types = TypeUtility.GetSubClassInstanceable(type).ToList();
                types.Insert(0, null);
                this.types = types;
                //CurrentValue
                object currentValue = property.propertyType switch
                {
                    SerializedPropertyType.ManagedReference => property.managedReferenceValue,
                    SerializedPropertyType.ObjectReference => property.objectReferenceValue,
                    _ => throw new Exception(),
                };
                //CurrentIndex
                var index = types.IndexOf(currentValue?.GetType());
                //PopupField
                popupField = new PopupField<Type>(types, index, Format, Format);
                popupField.RegisterValueChangedCallback(OnValueChange);
                Add(popupField);
                //
                UpdatePropertyField(property);
                ArraySetUp();
            }
            private void ArraySetUp()
            {
                if (!isArray) return;
                RegisterCallbackOnce<GeometryChangedEvent>(OnGeometryChange);
            }
            private void OnGeometryChange(GeometryChangedEvent e)
            {
                var listView = this.QParent<ListView>();
                if (listView == null) return;
                listView.onRemove = OnItemRemove;
                listView.onAdd = OnItemAdd;
            }
            private static void OnItemRemove(BaseListView view)
            {
                var sp = view.userData as SerializedProperty;
                UnityEngine.Object.DestroyImmediate(sp.GetArrayElementAtIndex(sp.arraySize - 1).objectReferenceValue);
                sp.DeleteArrayElementAtIndex(sp.arraySize - 1);
                sp.serializedObject.ApplyModifiedProperties();
                view.ScrollToItem(view.itemsSource.Count - 1);
            }
            private static void OnItemAdd(BaseListView view)
            {
                var sp = view.userData as SerializedProperty;
                sp.InsertArrayElementAtIndex(sp.arraySize);
                sp.GetArrayElementAtIndex(sp.arraySize - 1).boxedValue = null;
                sp.serializedObject.ApplyModifiedProperties();
                view.RefreshItems();
                view.ScrollToItem(view.itemsSource.Count - 1);
            }
            private void OnValueChange(ChangeEvent<Type> e)
            {
                //RemovePreviousValue
                if (property.propertyType == SerializedPropertyType.ObjectReference &&
                    property.objectReferenceValue != null)
                {
                    UnityEngine.Object.DestroyImmediate(property.objectReferenceValue);
                    property.objectReferenceValue = null;
                }
                //SetNewValue
                if (e.newValue != null)
                {
                    switch (property.propertyType)
                    {
                        case SerializedPropertyType.ManagedReference:
                            property.managedReferenceValue = Objects.New<object>(e.newValue);
                            break;
                        case SerializedPropertyType.ObjectReference:
                            var gameObject = (property.serializedObject.targetObject as Component)?.gameObject;
                            property.objectReferenceValue =
                                Objects.New<UnityEngine.Object>(e.newValue, null, Objects.ComponentNewAttribute.Instance, gameObject, HideFlags.HideInInspector);
                            break;
                    }
                }
                property.serializedObject.ApplyModifiedProperties();
                UpdatePropertyField(property);
            }
            private void UpdatePropertyField(SerializedProperty property)
            {
                if (propertyField != null) Remove(propertyField);
                propertyField = property.propertyType switch
                {
                    SerializedPropertyType.ManagedReference => new PropertyField(property),
                    SerializedPropertyType.ObjectReference =>
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
