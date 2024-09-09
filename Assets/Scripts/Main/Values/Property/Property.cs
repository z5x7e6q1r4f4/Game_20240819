using System;
namespace Main
{

    public abstract partial class Property<T> : IProperty<T>
    {
        protected abstract T SerializedProperty { get; set; }
        public T Value { get => GetValue(); set => SetValue(value); }
        public IObservable<PropertyBeforeSet<T>> BeforeSet => beforeSet;
        public IObservableImmediately<PropertyAfterSet<T>> AfterSet => afterSet;
        public IObservable<PropertyBeforeGet<T>> BeforeGet => beforeGet;
        private readonly EventHandler beforeSet;
        private readonly EventHandler afterSet;
        private readonly EventHandler beforeGet;
        public void SetValue(T value, bool beforeSet = true, bool afterSet = true)
        {
            var previous = SerializedProperty;
            if (beforeSet && !this.beforeSet.Invoke(this, PropertyEventArgsType.BeforeSet, previous, value, out value)) return;
            SerializedProperty = value;
            if (afterSet) this.afterSet.Invoke(this, PropertyEventArgsType.AfterSet, previous, value, out _);
        }
        public T GetValue(bool beforeGet = true)
        {
            var value = SerializedProperty;
            if (beforeGet) this.beforeGet.Invoke(this, PropertyEventArgsType.BeforeGet, value, value, out value);
            return value;
        }
        public Property(T value = default)
        {
            beforeSet = new();
            afterSet = new(AfterSetImmediately);
            beforeGet = new();
            Value = value;
        }
    }
}