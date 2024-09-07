using System;

namespace Main
{
    partial class ValueExtension
    {
        private sealed class OperatorToProperty<T> :
            Reuse.ObjectBase<OperatorToProperty<T>>,
            IOperatorToProperty<T>,
            IReuseable.IOnRelease
        {
            private IDisposable disposable;
            private IProperty<T> property;
            //Property
            T IPropertyReadonly<T>.Value => property.Value;
            IObservableImmediately<PropertyAfterSet<T>> IPropertyReadonly<T>.AfterSet => property.AfterSet;
            T IPropertyReadonly<T>.GetValue(bool beforeGet) => property.GetValue(beforeGet);
            //Reuse
            void IDisposable.Dispose() => this.ReleaseToReusePool();
            void IReuseable.IOnRelease.OnRelease()
            {
                disposable.Dispose();
                disposable = null;
                property.Value = default;
                property = null;
            }
            public static OperatorToProperty<T> GetFromReusePool(IDisposable disposable, IProperty<T> property)
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