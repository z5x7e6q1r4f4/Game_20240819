using Main.Editor;
using System;
using UnityEngine;

namespace Main 
{
    [Serializable]
    public class PropertySerializeReferenceSubClassSelector<T> : Property<T>, IValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeReference, SubClassSelector, SerializedProperty] private T serializedProperty;
        public PropertySerializeReferenceSubClassSelector(T value = default) : base(value) { }
    }
    [Serializable]
    public class PropertySerializeFieldSubClassSelector<T> : Property<T>, IValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeField, SubClassSelector, SerializedProperty] private T serializedProperty;
        public PropertySerializeFieldSubClassSelector(T value = default) : base(value) { }
    }
}