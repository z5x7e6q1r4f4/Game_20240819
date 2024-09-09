using UnityEditor;
using UnityEngine;
namespace Main.Reactive
{
    public static class ReuseEditor
    {
        [MenuItem("Reuse/Clear")]
        public static void Clear()
        {
            Reuse.Clear();
            Debug.Log("=== Cleared ===");
        }

        [MenuItem("Reuse/Log")]
        public static void Log()
        {
            Reuse.Log();
            Debug.Log("=== Log End ===");
        }
    }
}