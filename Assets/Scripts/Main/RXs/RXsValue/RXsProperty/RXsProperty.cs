using System;
namespace Main.RXs
{

    public abstract partial class RXsProperty<T> : IRXsProperty<T>
    {
        protected abstract T SerializedProperty { get; set; }
        public T Value { get => SerializedProperty; set => SetValue(value); }
        public IRXsObservable<IRXsProperty_BeforeSet<T>> BeforeSet => beforeSet;
        public IObservableImmediately<IRXsProperty_AfterSet<T>> AfterSet => afterSet;
        private readonly EventHandler beforeSet;
        private readonly EventHandler afterSet;
        public void SetValue(T value, bool beforeSet = true, bool afterSet = true)
        {
            var previous = SerializedProperty;
            if (beforeSet && !this.beforeSet.Invoke(this, previous, value, out value)) return;
            SerializedProperty = value;
            if (afterSet) this.afterSet.Invoke(this, previous, value, out _);
        }
        public RXsProperty(T value = default)
        {
            beforeSet = new();
            afterSet = new(AfterSetImmediately);
            Value = value;
        }
    }
}