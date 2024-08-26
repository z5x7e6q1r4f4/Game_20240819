using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Main
{
    [CustomPropertyDrawer(typeof(SubGameComponent), true)]
    public class SubGameComponentPropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
            => new SubGameComponentElement(property);
        private class SubGameComponentElement : VisualElement
        {
            public SubGameComponentElement(SerializedProperty property)
            {
                RegisterCallback<SerializedPropertyChangeEvent>(e =>
                {
                    if (SerializedProperty.EqualContents(e.changedProperty, property))
                        OnPropertyChange(e.changedProperty);
                });
                OnPropertyChange(property);
            }
            private void OnPropertyChange(SerializedProperty property)
            {
                Clear();
                if (property.objectReferenceValue == null)
                {
                    Add(new PropertyField(property));
                }
                else
                {
                    var so = new SerializedObject(property.objectReferenceValue);
                }
            }
        }
    }
}