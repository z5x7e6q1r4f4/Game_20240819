using System;

namespace Main
{
    public abstract class GameComponentSingleton<T> : GameComponent
        where T : GameComponentSingleton<T>
    {
        protected static T Instance => instance = instance != null ? instance : FindFirstObjectByType<T>().AwakeSelf<T>();
        private static T instance;
        protected override void OnGameComponentAwake()
        {
            if (instance != null && instance != this) throw new Exception("Singleton is not empty");
        }
        protected override void OnGameComponentDestroy()
        { if (instance == this) instance = null; }
    }
}