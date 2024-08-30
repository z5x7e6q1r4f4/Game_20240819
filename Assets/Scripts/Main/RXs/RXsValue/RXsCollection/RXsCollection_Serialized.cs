using Main.RXs.Unity;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.RXs
{
    [Serializable]
    public class RXsCollection_SerializeField<T> : RXsCollection<T>, IRXsValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeField, SerializedCollection] private List<T> serializedCollection = new();
        public RXsCollection_SerializeField(IEnumerable<T> collection = null) : base(collection) { }
    }
    [Serializable]
    public class RXsCollection_SerializeReference<T> : RXsCollection<T>, IRXsValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeReference, SerializedCollection] private List<T> serializedCollection = new();
        public RXsCollection_SerializeReference(IEnumerable<T> collection = null) : base(collection) { }
    }
    [Serializable]
    public class RXsCollection_SerializeReference_SubClassSelector<T> : RXsCollection<T>, IRXsValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeReference, SubClassSelector, SerializedCollection] private List<T> serializedCollection = new();
        public RXsCollection_SerializeReference_SubClassSelector(IEnumerable<T> collection = null) : base(collection) { }
    }
    [Serializable]
    public class RXsCollection_SerializeField_SubClassSelector<T> : RXsCollection<T>, IRXsValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeField, SubClassSelector, SerializedCollection] private List<T> serializedCollection = new();
        public RXsCollection_SerializeField_SubClassSelector(IEnumerable<T> collection = null) : base(collection) { }
    }
}