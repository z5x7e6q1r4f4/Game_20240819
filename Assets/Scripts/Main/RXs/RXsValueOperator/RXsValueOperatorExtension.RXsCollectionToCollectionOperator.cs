using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public static partial class RXsValueOperatorExtension
    {
        private abstract class RXsCollectionToCollectionOperator<TSource, TResult> :
            RXsCollectionToOperator<TSource>,
            IOperatorToCollection<TResult>
        {
            protected IRXsCollection<TResult> Result { get; }
            IObservableImmediately<IRXsCollection_AfterAdd<TResult>> IRXsCollection_Readonly<TResult>.AfterAdd => Result.AfterAdd;
            IObservable<IRXsCollection_AfterRemove<TResult>> IRXsCollection_Readonly<TResult>.AfterRemove => Result.AfterRemove;
            public int Count => Result.Count;
            TResult IRXsCollection_Readonly<TResult>.this[int index] => Result[index];
            bool IRXsCollection_Readonly<TResult>.Contains(TResult item) => Result.Contains(item);
            int IRXsCollection_Readonly<TResult>.IndexOf(TResult item) => Result.IndexOf(item);
            public IEnumerator<TResult> GetEnumerator() => Result.GetEnumerator();
            TResult IRXsCollection_Readonly<TResult>.GetAt(int index, bool indexCheck) => Result.GetAt(index, indexCheck);
            protected RXsCollectionToCollectionOperator(IRXsCollection_Readonly<TSource> source, IRXsCollection<TResult> result = null) : base(source) => Result = result ?? new RXsCollection_SerializeField<TResult>();
        }
        private abstract class RXsCollectionToCollectionOperator<TResult> :
            RXsCollectionToOperator,
            IOperatorToCollection<TResult>
        {
            protected IRXsCollection<TResult> Result { get; }
            IObservableImmediately<IRXsCollection_AfterAdd<TResult>> IRXsCollection_Readonly<TResult>.AfterAdd => Result.AfterAdd;
            IObservable<IRXsCollection_AfterRemove<TResult>> IRXsCollection_Readonly<TResult>.AfterRemove => Result.AfterRemove;
            public int Count => Result.Count;
            TResult IRXsCollection_Readonly<TResult>.this[int index] => Result[index];
            bool IRXsCollection_Readonly<TResult>.Contains(TResult item) => Result.Contains(item);
            int IRXsCollection_Readonly<TResult>.IndexOf(TResult item) => Result.IndexOf(item);
            public IEnumerator<TResult> GetEnumerator() => Result.GetEnumerator();
            TResult IRXsCollection_Readonly<TResult>.GetAt(int index, bool indexCheck) => Result.GetAt(index, indexCheck);
            protected RXsCollectionToCollectionOperator(IRXsCollection_Readonly source, IRXsCollection<TResult> result = null) : base(source) => Result = result ?? new RXsCollection_SerializeField<TResult>();
        }
    }
}