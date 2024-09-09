using Main.Editor;
using System.Collections.Generic;
using System;
using UnityEngine;
namespace Main
{

    [Serializable]
    public class CollectionSerializeReferenceSubClassSelector<T> : Collection<T>, IValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeReference, SubClassSelector, SerializedCollection] private List<T> serializedCollection = new();
        public CollectionSerializeReferenceSubClassSelector(IEnumerable<T> collection = null) : base(collection) { }
    }
    [Serializable]
    public class CollectionSerializeFieldSubClassSelector<T> : Collection<T>, IValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeField, SubClassSelector, SerializedCollection] private List<T> serializedCollection = new();
        public CollectionSerializeFieldSubClassSelector(IEnumerable<T> collection = null) : base(collection) { }
    }
}