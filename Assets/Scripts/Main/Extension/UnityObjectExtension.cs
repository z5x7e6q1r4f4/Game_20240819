namespace Main
{
    public static class UnityObjectExtension
    {
        public static void Destroy(this UnityEngine.Object obj, float delay = 0) => UnityEngine.Object.Destroy(obj, delay);
        public static void DestroyImmediate(this UnityEngine.Object obj, bool allowDestroyingAssets = false) => UnityEngine.Object.DestroyImmediate(obj, allowDestroyingAssets);
    }
}