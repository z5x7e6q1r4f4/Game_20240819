using System;

namespace Main.RXs
{
    public interface IRXsProperty : IRXsProperty_Readonly
    {
        new object Value { get; set; }
        void SetValue(object value, bool beforeSet = true, bool afterSet = true);
        IRXsObservable<IRXsProperty_BeforeSet> BeforeSet { get; }
        IRXsObservable<IRXsProperty_BeforeGet> BeforeGet { get; }
    }
    public interface IRXsProperty<T> :
        IRXsProperty_Readonly<T>,
        IRXsProperty
    {
        object IRXsProperty.Value { get => Value; set => Value = (T)value; }
        void IRXsProperty.SetValue(object value, bool beforeSet, bool afterSet) => SetValue((T)value, beforeSet, afterSet);
        IRXsObservable<IRXsProperty_BeforeSet> IRXsProperty.BeforeSet => BeforeSet;
        IRXsObservable<IRXsProperty_BeforeGet> IRXsProperty.BeforeGet => BeforeGet;
        //
        new T Value { get; set; }
        void SetValue(T value, bool beforeSet = true, bool afterSet = true);
        new IRXsObservable<IRXsProperty_BeforeSet<T>> BeforeSet { get; }
        new IRXsObservable<IRXsProperty_BeforeGet<T>> BeforeGet { get; }
    }
}