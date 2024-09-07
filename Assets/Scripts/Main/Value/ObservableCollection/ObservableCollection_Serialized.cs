using Main.RXs.Editor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main.RXs
{
    [Serializable]
    public class ObservableCollection_SerializeField<T> : ObservableCollectionBase<T>, IRXsValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeField, SerializedCollection] private List<T> serializedCollection = new();
        public ObservableCollection_SerializeField(IEnumerable<T> collection = null) : base(collection) { }
    }
    [Serializable]
    public class ObservableCollection_SerializeReference<T> : ObservableCollectionBase<T>, IRXsValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeReference, SerializedCollection] private List<T> serializedCollection = new();
        public ObservableCollection_SerializeReference(IEnumerable<T> collection = null) : base(collection) { }
    }
    [Serializable]
    public class ObservableCollection_SerializeReference_SubClassSelector<T> : ObservableCollectionBase<T>, IRXsValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeReference, SubClassSelector, SerializedCollection] private List<T> serializedCollection = new();
        public ObservableCollection_SerializeReference_SubClassSelector(IEnumerable<T> collection = null) : base(collection) { }
    }
    [Serializable]
    public class ObservableCollection_SerializeField_SubClassSelector<T> : ObservableCollectionBase<T>, IRXsValueInspector
    {
        protected override List<T> SerializedCollection => serializedCollection;
        [SerializeField, SubClassSelector, SerializedCollection] private List<T> serializedCollection = new();
        public ObservableCollection_SerializeField_SubClassSelector(IEnumerable<T> collection = null) : base(collection) { }
    }
}