using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.RXs
{
    [Serializable]
    public class RXsCollection_SerializeField<T> : RXsCollection<T>
    {
        [field: SerializeField] protected override List<T> SerializedCollection { get; } = new();
        public RXsCollection_SerializeField(IEnumerable<T> collection = null) : base(collection) { }
    }
    [Serializable]
    public class RXsCollection_SerializeReference<T> : RXsCollection<T>
    {
        [field: SerializeReference] protected override List<T> SerializedCollection { get; } = new();
        public RXsCollection_SerializeReference(IEnumerable<T> collection = null) : base(collection) { }
    }
}