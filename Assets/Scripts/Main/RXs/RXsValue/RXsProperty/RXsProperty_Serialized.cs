using System;
using UnityEngine;
using Main.RXs.Unity;
namespace Main.RXs
{
    [Serializable]
    public class RXsProperty_SerializeField<T> : RXsProperty<T>, IRXsValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeField, SerializedProperty] private T serializedProperty;
        public RXsProperty_SerializeField(T value = default) : base(value) { }
    }
    [Serializable]
    public class RXsProperty_SerializeReference<T> : RXsProperty<T>, IRXsValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeReference, SerializedProperty] private T serializedProperty;
        public RXsProperty_SerializeReference(T value = default) : base(value) { }
    }
    [Serializable]
    public class RXsProperty_SerializeReference_SubClassSelector<T> : RXsProperty<T>, IRXsValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeReference, SubClassSelector, SerializedProperty] private T serializedProperty;
        public RXsProperty_SerializeReference_SubClassSelector(T value = default) : base(value) { }
    }
    [Serializable]
    public class RXsProperty_SerializeField_SubClassSelector<T> : RXsProperty<T>, IRXsValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeField, SubClassSelector, SerializedProperty] private T serializedProperty;
        public RXsProperty_SerializeField_SubClassSelector(T value = default) : base(value) { }
    }
}