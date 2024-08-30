using System;

namespace Main.RXs
{
    partial class RXsValueOperatorExtension
    {
        private class ConnectCollectionToCollectionOperator<T> : RXsCollectionToCollectionOperator<T, T>
        {
            public ConnectCollectionToCollectionOperator(IRXsCollection_Readonly<T> source, IRXsCollection<T> result = null) : base(source, result) => Subscribe();
            protected override void AfterAdd(IRXsCollection_AfterAdd<T> e) => Result.Add(e.Item);
            protected override void AfterRemove(IRXsCollection_AfterRemove<T> e) => Result.Remove(e.Item);
        }
        private class ConnectPropertyToPropertyOperator<T> : RXsPropertyToPropertyOperator<T, T>
        {
            public ConnectPropertyToPropertyOperator(IRXsProperty_Readonly<T> source, IRXsProperty<T> result = null) : base(source, result) => Subscribe();
            protected override void AfterSet(IRXsProperty_AfterSet<T> e) => Result.Value = e.Current;
        }
        public static IDisposable ConnectTo<T>(this IRXsCollection_Readonly<T> source, IRXsCollection<T> result)
            => new ConnectCollectionToCollectionOperator<T>(source, result);
        public static IDisposable ConnectFrom<T>(this IRXsCollection<T> result, IRXsCollection_Readonly<T> source)
            => new ConnectCollectionToCollectionOperator<T>(source, result);
        public static IDisposable ConnectTo<T>(this IRXsProperty_Readonly<T> source, IRXsProperty<T> result)
            => new ConnectPropertyToPropertyOperator<T>(source, result);
        public static IDisposable ConnectFrom<T>(this IRXsProperty<T> result, IRXsProperty_Readonly<T> source)
            => new ConnectPropertyToPropertyOperator<T>(source, result);
    }
}