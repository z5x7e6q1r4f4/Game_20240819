using System;
using UnityEngine;
namespace Main.RXs
{
    [Serializable]
    public class RXsProperty_SerializeField<T> : RXsProperty<T>
    {
        [field: SerializeField] protected override T SerializedProperty { get; set; }
        public RXsProperty_SerializeField(T value = default) : base(value) { }
    }
    [Serializable]
    public class RXsProperty_SerializeReference<T> : RXsProperty<T>
    {
        [field: SerializeReference] protected override T SerializedProperty { get; set; }
        public RXsProperty_SerializeReference(T value = default) : base(value) { }
    }
}