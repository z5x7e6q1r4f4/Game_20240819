using System;

namespace Main.RXs
{
    partial class Operation
    {
        private sealed class OperatorToProperty<T> :
            Reuse.ObjectBase<OperatorToProperty<T>>,
            IOperatorToProperty<T>,
            IReuseable.IOnRelease
        {
            private IDisposable disposable;
            private IObservableProperty<T> property;
            //Property
            T IObservableProperty_Readonly<T>.Value => property.Value;
            IObservableImmediately<IObservableProperty_AfterSet<T>> IObservableProperty_Readonly<T>.AfterSet => property.AfterSet;
            T IObservableProperty_Readonly<T>.GetValue(bool beforeGet) => property.GetValue(beforeGet);
            //Reuse
            void IDisposable.Dispose() => this.ReleaseToReusePool();
            void IReuseable.IOnRelease.OnRelease()
            {
                disposable.Dispose();
                disposable = null;
                property.Value = default;
                property = null;
            }
            public static OperatorToProperty<T> GetFromReusePool(IDisposable disposable, IObservableProperty<T> property)
            {
                var propertySubscription = GetFromReusePool();
                propertySubscription.disposable = disposable;
                propertySubscription.property = property;
                return propertySubscription;
            }
            private OperatorToProperty() { }
        }
    }
}