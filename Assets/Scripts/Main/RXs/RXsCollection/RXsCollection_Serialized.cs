using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.RXs
{
    [Serializable]
    public class RXsCollection_SerializeField<T> : RXsCollection<T>
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeField] private readonly List<T> serializedCollection = new();
        public RXsCollection_SerializeField(IEnumerable<T> collection = null) : base(collection) { }
    }
    [Serializable]
    public class RXsCollection_SerializeReference<T> : RXsCollection<T>
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeReference] private readonly List<T> serializedCollection = new();
        public RXsCollection_SerializeReference(IEnumerable<T> collection = null) : base(collection) { }
    }
}