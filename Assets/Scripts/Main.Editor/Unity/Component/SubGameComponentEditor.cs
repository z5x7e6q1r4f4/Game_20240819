using UnityEditor;

namespace Main
{
    [CustomEditor(typeof(SubGameComponent), editorForChildClasses: true)]
    public class SubGameComponentEditor : Editor
    {
        SubGameComponent SubGameComponent => target as SubGameComponent;
        private void OnEnable()
        {
            SubGameComponent.hideFlags = UnityEngine.HideFlags.HideInInspector;
        }
    }
}