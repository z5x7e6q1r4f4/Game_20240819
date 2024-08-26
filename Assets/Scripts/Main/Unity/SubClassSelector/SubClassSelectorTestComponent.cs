using System;
using System.Collections.Generic;
using UnityEngine;

namespace Main
{
    public class SubClassSelectorTestComponent : MonoBehaviour
    {
        [SubClassSelector, SerializeReference] public Bass value;
        [SubClassSelector, SerializeReference] public List<Bass> values;
        [Serializable]
        public class Bass 
        {
        }
        [Serializable]
        public class A : Bass
        {
            public float floatValue;
        }
        [Serializable]
        public class B : Bass 
        {
            public string stringValue;
        }
        [Serializable]
        public class A2 : A 
        {
        }
        [Serializable]
        public class B2 : B 
        {
        }
    }
}
