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
            private IRXsSubscription subscription;
            private IRXsProperty<T> property;
            //Subscription
            void IRXsSubscription.Subscribe() => subscription.Subscribe();
            void IRXsSubscription.Unsubscribe()
            {
                subscription.Unsubscribe();
                property.Value = default;
            }
            //Property
            T IRXsProperty_Readonly<T>.Value => property.Value;
            IRXsObservableImmediately<IRXsProperty_AfterSet<T>> IRXsProperty_Readonly<T>.AfterSet => property.AfterSet;
            //Reuse
            void IDisposable.Dispose() => this.ReleaseToReusePool();
            void IReuseable.IOnRelease.OnRelease()
            {
                subscription.Dispose();
                subscription = null;
                property.Value = default;
                property = null;
            }
            public static RXsOperatorToProperty<T> GetFromReusePool(IRXsSubscription subscription, IRXsProperty<T> property)
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