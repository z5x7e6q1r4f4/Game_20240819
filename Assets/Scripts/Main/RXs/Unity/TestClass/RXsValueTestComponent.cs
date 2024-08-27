using UnityEngine;

namespace Main.RXs.Unity
{
    public class RXsValueTestComponent : MonoBehaviour
    {
        public RXsCollection_SerializeField<string> serializeFieldStringCollection = new();
        public RXsProperty_SerializeField<string> serializeFieldStringProperty = new();
        public RXsCollection_SerializeReference_SubClassSelector<CSharpClassBase> CSharpClasses = new();
        public RXsCollection_SerializeField_SubClassSelector<MonoClassBase> MonoClasses = new();
        public RXsCollection_SerializeField_SubClassSelector<ScriptableObjectBase> ScriptableObjectClasses = new();
    }
}