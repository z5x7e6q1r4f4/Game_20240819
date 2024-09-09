using System;

namespace Main
{
    public interface IProperty : IPropertyReadonly
    {
        new object Value { get; set; }
        void SetValue(object value, bool beforeSet = true, bool afterSet = true);
        IObservable<PropertyBeforeSet> BeforeSet { get; }
        IObservable<PropertyBeforeGet> BeforeGet { get; }
    }
    public interface IProperty<T> :
        IPropertyReadonly<T>,
        IProperty
    {
        object IProperty.Value { get => Value; set => Value = (T)value; }
        void IProperty.SetValue(object value, bool beforeSet, bool afterSet) => SetValue((T)value, beforeSet, afterSet);
        IObservable<PropertyBeforeSet> IProperty.BeforeSet => BeforeSet;
        IObservable<PropertyBeforeGet> IProperty.BeforeGet => BeforeGet;
        //
        new T Value { get; set; }
        void SetValue(T value, bool beforeSet = true, bool afterSet = true);
        new IObservable<PropertyBeforeSet<T>> BeforeSet { get; }
        new IObservable<PropertyBeforeGet<T>> BeforeGet { get; }
    }
}