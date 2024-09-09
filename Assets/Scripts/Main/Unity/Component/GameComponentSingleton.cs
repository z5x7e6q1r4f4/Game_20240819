using System;
using UnityEngine;

namespace Main
{
    public abstract class GameComponentSingleton<T> : GameComponent
        where T : GameComponentSingleton<T>
    {
        protected static T Instance => instance ??= CreateGameComponentSingleton();
        private static T instance;
        private static T CreateGameComponentSingleton()
            => FindFirstObjectByType<T>() ?? Objects.New<T>(typeof(T));
        protected override void OnGameComponentAwake()
        { if (instance != null && instance != this) throw new Exception("Singleton is not empty"); }
        protected override void OnGameComponentDestroy()
        { if (instance == this) instance = null; }
    }
}