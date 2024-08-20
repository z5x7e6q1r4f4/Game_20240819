using System;

namespace Main.RXs
{
    public interface IRXsProperty
    {
        object Value { get; set; }
        void SetValue(object value, bool beforeSet = true, bool afterSet = true);
        IObservable<IRXsProperty_BeforeSet> BeforeSet { get; }
    }
    public interface IRXsProperty<T> :
        IRXsProperty_Readonly<T>,
        IRXsProperty
    {
        object IRXsProperty.Value { get => Value; set => Value = (T)value; }
        void IRXsProperty.SetValue(object value, bool beforeSet, bool afterSet) => SetValue((T)value, beforeSet, afterSet);
        IObservable<IRXsProperty_BeforeSet> IRXsProperty.BeforeSet => BeforeSet;
        //
        new T Value { get; set; }
        void SetValue(T value, bool beforeSet = true, bool afterSet = true);
        new IObservable<IRXsProperty_BeforeSet<T>> BeforeSet { get; }
    }
}