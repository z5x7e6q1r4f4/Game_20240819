using System;
using UnityEngine;
using Main.Editor;
namespace Main
{
    [Serializable]
    public class PropertySerializeField<T> : Property<T>, IValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeField, SerializedProperty] private T serializedProperty;
        public PropertySerializeField(T value = default) : base(value) { }
    }
    [Serializable]
    public class PropertySerializeReference<T> : Property<T>, IValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeReference, SerializedProperty] private T serializedProperty;
        public PropertySerializeReference(T value = default) : base(value) { }
    }
}