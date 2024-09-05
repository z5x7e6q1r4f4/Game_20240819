using System;

namespace Main.RXs
{
    partial class RXsOperation
    {
        private sealed class RXsOperatorToProperty<T> :
            Reuse.ObjectBase<RXsOperatorToProperty<T>>,
            IRXsOperatorToProperty<T>,
            IReuseable.IOnRelease
        {
            private IRXsDisposable subscription;
            private IRXsProperty<T> property;
            //Property
            T IRXsProperty_Readonly<T>.Value => property.Value;
            IRXsObservableImmediately<IRXsProperty_AfterSet<T>> IRXsProperty_Readonly<T>.AfterSet => property.AfterSet;
            T IRXsProperty_Readonly<T>.GetValue(bool beforeGet) => property.GetValue(beforeGet);
            //Reuse
            void IDisposable.Dispose() => this.ReleaseToReusePool();
            void IReuseable.IOnRelease.OnRelease()
            {
                subscription.Dispose();
                subscription = null;
                property.Value = default;
                property = null;
            }
            public static RXsOperatorToProperty<T> GetFromReusePool(IRXsDisposable subscription, IRXsProperty<T> property)
            {
                var propertySubscription = GetFromReusePool();
                propertySubscription.subscription = subscription;
                propertySubscription.property = property;
                return propertySubscription;
            }
            private RXsOperatorToProperty() { }
        }
    }
}