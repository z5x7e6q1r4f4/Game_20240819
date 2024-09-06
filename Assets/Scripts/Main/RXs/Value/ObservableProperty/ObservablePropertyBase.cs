using Main.RXs.RXsProperties;
using System;
namespace Main.RXs
{

    public abstract partial class ObservablePropertyBase<T> : IObservableProperty<T>
    {
        protected abstract T SerializedProperty { get; set; }
        public T Value { get => GetValue(); set => SetValue(value); }
        public IObservable<IObservableProperty_BeforeSet<T>> BeforeSet => beforeSet;
        public IObservableImmediately<IObservableProperty_AfterSet<T>> AfterSet => afterSet;
        public IObservable<IObservableProperty_BeforeGet<T>> BeforeGet => beforeGet;
        private readonly EventHandler beforeSet;
        private readonly EventHandler afterSet;
        private readonly EventHandler beforeGet;
        public void SetValue(T value, bool beforeSet = true, bool afterSet = true)
        {
            var previous = SerializedProperty;
            if (beforeSet && !this.beforeSet.Invoke(this, ObservablePropertyEventArgsType.BeforeSet, previous, value, out value)) return;
            SerializedProperty = value;
            if (afterSet) this.afterSet.Invoke(this, ObservablePropertyEventArgsType.AfterSet, previous, value, out _);
        }
        public T GetValue(bool beforeGet = true)
        {
            var value = SerializedProperty;
            if (beforeGet) this.beforeGet.Invoke(this, ObservablePropertyEventArgsType.BeforeGet, value, value, out value);
            return value;
        }
        public ObservablePropertyBase(T value = default)
        {
            beforeSet = new();
            afterSet = new(AfterSetImmediately);
            beforeGet = new();
            Value = value;
        }
    }
}