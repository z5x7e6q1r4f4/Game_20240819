using UnityEngine;

namespace Main
{
    public class GameComponentSingleton<T> : GameComponent
        where T : GameComponentSingleton<T>
    {
        protected static T Instance => instance = instance != null ? instance : FindFirstObjectByType<T>().AwakeSelf<T>();
        private static T instance;
        protected override void OnGameComponentDestroy()
        { if (instance == this) instance = null; }
    }
}