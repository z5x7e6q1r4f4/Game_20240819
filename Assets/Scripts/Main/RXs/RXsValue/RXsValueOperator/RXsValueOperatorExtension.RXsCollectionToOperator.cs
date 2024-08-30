using System;

namespace Main.RXs
{
    public static partial class RXsValueOperatorExtension
    {
        private abstract class RXsCollectionToOperator<TSource> : IDisposable
        {
            protected virtual int Order => default;
            protected IRXsCollection_Readonly<TSource> Source { get; }
            private readonly DisposableList disposableList = new();
            public RXsCollectionToOperator(IRXsCollection_Readonly<TSource> source) => Source = source;
            public virtual void Subscribe()
            {
                if (disposableList.Count != 0) return;
                disposableList.Add(
                    Source.AfterAdd.Immediately().Order(Order).Subscribe(AfterAdd)
                    );
                disposableList.Add(
                    Source.AfterRemove.Order(Order).Subscribe(AfterRemove)
                    );
            }
            public virtual void Dispose() => disposableList.Dispose();
            protected abstract void AfterRemove(IRXsCollection_AfterRemove<TSource> e);
            protected abstract void AfterAdd(IRXsCollection_AfterAdd<TSource> e);
        }
        private abstract class RXsCollectionToOperator : IDisposable
        {
            protected virtual int Order => default;
            protected IRXsCollection_Readonly Source { get; }
            private readonly DisposableList disposableList = new();
            public RXsCollectionToOperator(IRXsCollection_Readonly source) => Source = source;
            public virtual void Subscribe()
            {
                if (disposableList.Count != 0) return;
                disposableList.Add(
                    Source.AfterAdd.Immediately().Order(Order).Subscribe(AfterAdd)
                    );
                disposableList.Add(
                    Source.AfterRemove.Order(Order).Subscribe(AfterRemove)
                    );
            }
            public virtual void Dispose() => disposableList.Dispose();
            protected abstract void AfterRemove(IRXsCollection_AfterRemove e);
            protected abstract void AfterAdd(IRXsCollection_AfterAdd e);
        }
    }
}