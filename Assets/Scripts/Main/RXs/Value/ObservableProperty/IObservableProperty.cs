using System;

namespace Main.RXs
{
    public interface IObservableProperty : IObservableProperty_Readonly
    {
        new object Value { get; set; }
        void SetValue(object value, bool beforeSet = true, bool afterSet = true);
        IObservable<IObservableProperty_BeforeSet> BeforeSet { get; }
        IObservable<IObservableProperty_BeforeGet> BeforeGet { get; }
    }
    public interface IObservableProperty<T> :
        IObservableProperty_Readonly<T>,
        IObservableProperty
    {
        object IObservableProperty.Value { get => Value; set => Value = (T)value; }
        void IObservableProperty.SetValue(object value, bool beforeSet, bool afterSet) => SetValue((T)value, beforeSet, afterSet);
        IObservable<IObservableProperty_BeforeSet> IObservableProperty.BeforeSet => BeforeSet;
        IObservable<IObservableProperty_BeforeGet> IObservableProperty.BeforeGet => BeforeGet;
        //
        new T Value { get; set; }
        void SetValue(T value, bool beforeSet = true, bool afterSet = true);
        new IObservable<IObservableProperty_BeforeSet<T>> BeforeSet { get; }
        new IObservable<IObservableProperty_BeforeGet<T>> BeforeGet { get; }
    }
}