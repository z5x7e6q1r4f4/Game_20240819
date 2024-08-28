using System;
using System.Collections.Generic;
using System.Linq;
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
            => new SubClassSelectorElement(property, attribute as SubClassSelectorAttribute);
        private class SubClassSelectorElement : VisualElement
        {
            //Data
            protected SerializedProperty Property { get; }
            protected SubClassSelectorAttribute Attribute { get; }
            protected List<Type> SubClassTypes { get; }
            //VisualElement
            protected Foldout Foldout { get; }
            protected VisualElement HeaderContainer { get; }
            protected PopupField<Type> PopupField { get; }
            protected VisualElement PropertyContainer => Foldout.contentContainer;
            public SubClassSelectorElement(
                SerializedProperty property,
                SubClassSelectorAttribute attribute)
            {
                Property = property;
                Attribute = attribute;
                SubClassTypes = CreateSubClassTypes();
                Foldout = CreateFoldout();
                Add(Foldout);
                HeaderContainer = CreateHeaderContainer(Foldout);
                PopupField = CreatePopupField();
                HeaderContainer.Add(PopupField);
                UpdatePropertyField();
                if (Property.IsArrayElement()) RegisterCallbackOnce<GeometryChangedEvent>(OnGeometryChange);
            }
            protected virtual List<Type> CreateSubClassTypes()
            {
                var type = Property.GetPropertyType();
                type = Attribute.type ?? type;
                var result = TypeUtility.GetSubClassInstanceable(type).ToList();
                result.Insert(0, null);
                return result;
            }
            protected virtual Foldout CreateFoldout() => new();
            protected virtual VisualElement CreateHeaderContainer(Foldout foldout)
            {
                var headerContainer = new VisualElement();
                foldout.Q<Toggle>().Query<VisualElement>().AtIndex(1).Add(headerContainer);
                headerContainer.style.flexGrow = 1;
                headerContainer.style.flexDirection = FlexDirection.Row;
                return headerContainer;
            }
            protected virtual PopupField<Type> CreatePopupField()
            {
                object currentValue = Property.GetValue<object>();
                var popupField = new PopupField<Type>(SubClassTypes, SubClassTypes.IndexOf(currentValue?.GetType()), Format, Format);
                popupField.RegisterValueChangedCallback(OnValueChange);
                popupField.style.flexGrow = 1;
                return popupField;
            }
            protected virtual void UpdatePropertyField()
            {
                PropertyContainer.Clear();
                var unityObject = Property.GetValue<UnityEngine.Object>();
                if (unityObject != null)
                {
                    InspectorElement inspectorElement = new(unityObject);
                    inspectorElement.style.Padding(1);
                    PropertyContainer.Add(inspectorElement);
                }
                else if (Property.propertyType == SerializedPropertyType.ManagedReference)
                {
                    foreach (var child in Property.GetChildren()) PropertyContainer.Add(new PropertyField(child));
                }
            }
            protected virtual void OnGeometryChange(GeometryChangedEvent e)
            {
                var listView = this.QParent<ListView>();
                if (listView == null) return;
                SetUpList(listView);
            }
            protected virtual void OnValueChange(ChangeEvent<Type> e)
            {
                DestoryPreviousPropertyValue();
                SetNewPropertyValue(e.newValue);
                Property.serializedObject.ApplyModifiedProperties();
                UpdatePropertyField();
            }
            protected virtual void DestoryPreviousPropertyValue()
            {
                Property.GetValue<UnityEngine.Object>()?.DestroyImmediate();
                Property.SetValue(null);
            }
            protected virtual void SetNewPropertyValue(Type type)
            {
                if (type != null)
                {
                    var newValue = typeof(Component).IsAssignableFrom(type) ?
                        Objects.New<object>(type, null, Property.GetGameObject(), HideFlags.HideInInspector) :
                        Objects.New<object>(type);
                    Property.SetValue(newValue);
                }
            }
            protected virtual void SetUpList(ListView listView)
            {
                listView.onRemove = OnItemRemove;
                listView.onAdd = OnItemAdd;
                listView.showBoundCollectionSize = false;
                HeaderContainer.Add(CreateRemoveElementButton());
            }
            protected virtual Button CreateRemoveElementButton()
            {
                var button = new Button(() =>
                {
                    DestoryPreviousPropertyValue();
                    var parent = Property.GetParent();
                    parent.DeleteArrayElementAtIndex(Property.GetIndex());
                    parent.serializedObject.ApplyModifiedProperties();
                });
                button.text = "Remove";
                button.style.paddingLeft = 8;
                button.style.marginLeft = 5;
                return button;
            }
            protected static void OnItemRemove(BaseListView view)
            {
                var sp = view.userData as SerializedProperty;
                sp.GetArrayElementAtIndex(sp.arraySize - 1).GetValue<UnityEngine.Object>()?.DestroyImmediate();
                sp.DeleteArrayElementAtIndex(sp.arraySize - 1);
                sp.serializedObject.ApplyModifiedProperties();
                view.ScrollToItem(view.itemsSource.Count - 1);
            }
            protected static void OnItemAdd(BaseListView view)
            {
                var sp = view.userData as SerializedProperty;
                sp.InsertArrayElementAtIndex(sp.arraySize);
                sp.GetArrayElementAtIndex(sp.arraySize - 1).SetValue(null);
                sp.serializedObject.ApplyModifiedProperties();
                view.ScrollToItem(view.itemsSource.Count - 1);
            }
            private string Format(Type type) => $"{type?.Name ?? "Null"}{(type != null ? $"({type.Namespace})" : string.Empty)}";
        }
    }
}
