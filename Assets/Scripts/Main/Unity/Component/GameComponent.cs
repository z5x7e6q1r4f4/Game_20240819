using Main.RXs;
using System;
using UnityEngine;

namespace Main
{
    public abstract partial class GameComponent : MonoBehaviour
    {
        public bool EnableDebug { get => enableDebug.Value; set => enableDebug.Value = value; }
        [SerializeField, DisableRXsValueDebug] private ObservableProperty_SerializeField<bool> enableDebug = new();
        //Event
        public IObservableImmediately<GameComponent> OnGameComponentAwakeEvent
            => onGameComponentAwakeEvent ??= new(observer => { if (hasAwake) observer.OnNext(this); });
        public IObservableImmediately<GameComponent> OnGameComponentEnableEvent
            => onGameComponentEnableEvent ??= new(observer => { if (isActiveAndEnabled && hasAwake) observer.OnNext(this); });
        public IObservableImmediately<GameComponent> OnGameComponentDisableEvent
            => onGameComponentDisableEvent ??= new(observer => { if (!isActiveAndEnabled) observer.OnNext(this); });
        public IObservable<GameComponent> OnGameComponentDestroyEvent
            => onGameComponentDestroyEvent ??= new();
        private ObservableEventHandler<GameComponent> onGameComponentAwakeEvent;
        private ObservableEventHandler<GameComponent> onGameComponentEnableEvent;
        private ObservableEventHandler<GameComponent> onGameComponentDisableEvent;
        private ObservableEventHandler<GameComponent> onGameComponentDestroyEvent;
        //LifeCycle
        private bool hasAwake = false;
        protected T AwakeSelf<T>() where T : GameComponent
        {
            if (!hasAwake)
            {
                hasAwake = true;
                AwakeGameComponentDebug();
                OnGameComponentAwake();
                onGameComponentAwakeEvent?.Invoke(this);
            }
            return (T)this;
        }
        private void AwakeGameComponentDebug()
        {
            enableDebug.AfterSet.
                Immediately().
                Where(e => e.Current).
                Subscribe(() =>
                    Operation.EnableDebugAllValueFrom(this).
                    Until(enableDebug.AfterSet.Immediately().Where(e => !e.Current))
                );
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
        public IObservableCollection_Readonly<GameComponent> GameComponentList => tracingList;
        private GameComponentTracingList tracingList => _tracingList ??= GetOrAddComponent<GameComponentTracingList>(isTrackable: false);
        [DisableRXsValueDebug] private GameComponentTracingList _tracingList;
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