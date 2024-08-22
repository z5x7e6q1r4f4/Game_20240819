using Main.RXs;
using System;
using UnityEngine;

namespace Main
{
    public partial class GameComponent : MonoBehaviour
    {
        //Event
        public IObservableImmediately<GameComponent> OnGameComponentAwakeEvent
            => onGameComponentAwakeEvent ??= new(this, CheckHasAwake);
        public IObservableImmediately<GameComponent> OnGameComponentEnableEvent
            => onGameComponentEnableEvent ??= new(this, CheckHasEnable);
        public IObservableImmediately<GameComponent> OnGameComponentDisableEvent
            => onGameComponentDisableEvent ??= new(this, CheckHasDisable);
        public RXs.IObservable<GameComponent> OnGameComponentDestroyEvent
            => onGameComponentDestroyEvent ??= new();
        private EventHandler onGameComponentAwakeEvent;
        private EventHandler onGameComponentEnableEvent;
        private EventHandler onGameComponentDisableEvent;
        private RXsEventHandler<GameComponent> onGameComponentDestroyEvent;
        private static bool CheckHasAwake(GameComponent gameComponent) => gameComponent.hasAwake;
        private static bool CheckHasEnable(GameComponent gameComponent) => gameComponent.isActiveAndEnabled;
        private static bool CheckHasDisable(GameComponent gameComponent) => !gameComponent.isActiveAndEnabled;
        //LifeCycle
        private bool hasAwake = false;
        protected T AwakeSelf<T>() where T : GameComponent
        {
            if (!hasAwake)
            {
                hasAwake = true;
                OnGameComponentAwake();
                onGameComponentAwakeEvent?.Invoke(this);
            }
            return (T)this;
        }
        protected virtual void OnGameComponentAwake() { }
        protected virtual void OnGameComponentEnable() { }
        protected virtual void OnGameComponentDisable() { }
        protected virtual void OnGameComponentDestroy() { }
        private void Awake() => AwakeSelf<GameComponent>();
        private void OnEnable()
        {
            OnGameComponentEnable();
            onGameComponentEnableEvent?.Invoke(this);
        }
        private void OnDisable()
        {
            OnGameComponentDisable();
            onGameComponentDisableEvent?.Invoke(this);
        }
        private void OnDestroy()
        {
            OnGameComponentDestroy();
            onGameComponentDestroyEvent?.Invoke(this);
        }
        //Component
        public IRXsCollection_Readonly<GameComponent> GameComponentList => tracingList;
        private GameComponentTracingList tracingList => _tracingList ??= GetOrAddComponent<GameComponentTracingList>(isTrackable: false);
        private GameComponentTracingList _tracingList;
        public T AddComponent<T>(HideFlags hideFlags = HideFlags.None, bool isTrackable = true)
            where T : Component
        {
            var component = gameObject.AddComponent<T>();
            component.hideFlags = hideFlags;
            if (isTrackable && component is GameComponent gameComponent) tracingList.Add(gameComponent);
            return component;
        }
        public T GetOrAddComponent<T>(HideFlags hideFlags = HideFlags.HideInInspector, bool isTrackable = true)
            where T : Component
            => GetComponent<T>() ?? AddComponent<T>(hideFlags, isTrackable);
        public void RemoveComponent<T>(T component, bool isTrackable = true)
        {
            component ??= GetComponent<T>();
            if (component == null) return;
#if UNITY_EDITOR
            if ((component as Component).gameObject != gameObject) throw new InvalidOperationException("component not on gameObject");
#endif
            if (isTrackable && component is GameComponent gameComponent) tracingList.Remove(gameComponent);
            Destroy(component as Component);
        }
    }
}