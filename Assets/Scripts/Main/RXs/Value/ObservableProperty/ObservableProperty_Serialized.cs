using System;
using UnityEngine;
using Main.RXs.Editor;
namespace Main.RXs
{
    [Serializable]
    public class ObservableProperty_SerializeField<T> : ObservablePropertyBase<T>, IRXsValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeField, SerializedProperty] private T serializedProperty;
        public ObservableProperty_SerializeField(T value = default) : base(value) { }
    }
    [Serializable]
    public class ObservableProperty_SerializeReference<T> : ObservablePropertyBase<T>, IRXsValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeReference, SerializedProperty] private T serializedProperty;
        public ObservableProperty_SerializeReference(T value = default) : base(value) { }
    }
    [Serializable]
    public class ObservableProperty_SerializeReference_SubClassSelector<T> : ObservablePropertyBase<T>, IRXsValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeReference, SubClassSelector, SerializedProperty] private T serializedProperty;
        public ObservableProperty_SerializeReference_SubClassSelector(T value = default) : base(value) { }
    }
    [Serializable]
    public class ObservableProperty_SerializeField_SubClassSelector<T> : ObservablePropertyBase<T>, IRXsValueInspector
    {
        protected override T SerializedProperty { get => serializedProperty; set => serializedProperty = value; }
        [SerializeField, SubClassSelector, SerializedProperty] private T serializedProperty;
        public ObservableProperty_SerializeField_SubClassSelector(T value = default) : base(value) { }
    }
}