using Main.Editor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    [Serializable]
    public class CollectionSerializeField<T> : Collection<T>, IValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeField, SerializedCollection] private List<T> serializedCollection = new();
        public CollectionSerializeField(IEnumerable<T> collection = null) : base(collection) { }
    }
    [Serializable]
    public class CollectionSerializeReference<T> : Collection<T>, IValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeReference, SerializedCollection] private List<T> serializedCollection = new();
        public CollectionSerializeReference(IEnumerable<T> collection = null) : base(collection) { }
    }

}