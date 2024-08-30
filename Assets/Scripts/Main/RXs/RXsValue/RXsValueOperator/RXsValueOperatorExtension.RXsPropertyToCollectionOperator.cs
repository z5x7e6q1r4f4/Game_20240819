using System.Collections.Generic;

namespace Main.RXs
{
    public static partial class RXsValueOperatorExtension
    {
        private abstract class RXsPropertyToCollectionOperator<TSource, TResult> :
            RXsPropertyToOperator<TSource>,
            IOperatorToCollection<TResult>
        {
            protected IRXsCollection<TResult> Result { get; }
            IObservableImmediately<IRXsCollection_AfterAdd<TResult>> IRXsCollection_Readonly<TResult>.AfterAdd => Result.AfterAdd;
            IRXsObservable<IRXsCollection_AfterRemove<TResult>> IRXsCollection_Readonly<TResult>.AfterRemove => Result.AfterRemove;
            int IRXsCollection_Readonly.Count => Result.Count;
            TResult IRXsCollection_Readonly<TResult>.this[int index] => Result[index];
            protected RXsPropertyToCollectionOperator(IRXsProperty_Readonly<TSource> source, IRXsCollection<TResult> result) : base(source) => Result = result ?? new RXsCollection_SerializeField<TResult>();
            bool IRXsCollection_Readonly<TResult>.Contains(TResult item) => Result.Contains(item);
            int IRXsCollection_Readonly<TResult>.IndexOf(TResult item) => Result.IndexOf(item);
            IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() => Result.GetEnumerator();
            TResult IRXsCollection_Readonly<TResult>.GetAt(int index, bool indexCheck) => Result.GetAt(index, indexCheck);
            public override void Dispose()
            {
                base.Dispose();
                Result.Clear();
            }
        }
        private abstract class RXsPropertyToCollectionOperator<TResult> :
         RXsPropertyToOperator,
         IOperatorToCollection<TResult>
        {
            protected IRXsCollection<TResult> Result { get; }
            IObservableImmediately<IRXsCollection_AfterAdd<TResult>> IRXsCollection_Readonly<TResult>.AfterAdd => Result.AfterAdd;
            IRXsObservable<IRXsCollection_AfterRemove<TResult>> IRXsCollection_Readonly<TResult>.AfterRemove => Result.AfterRemove;
            int IRXsCollection_Readonly.Count => Result.Count;
            TResult IRXsCollection_Readonly<TResult>.this[int index] => Result[index];
            protected RXsPropertyToCollectionOperator(IRXsProperty_Readonly source, IRXsCollection<TResult> result) : base(source) => Result = result ?? new RXsCollection_SerializeField<TResult>();
            bool IRXsCollection_Readonly<TResult>.Contains(TResult item) => Result.Contains(item);
            int IRXsCollection_Readonly<TResult>.IndexOf(TResult item) => Result.IndexOf(item);
            IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator() => Result.GetEnumerator();
            TResult IRXsCollection_Readonly<TResult>.GetAt(int index, bool indexCheck) => Result.GetAt(index, indexCheck);
            public override void Dispose()
            {
                base.Dispose();
                Result.Clear();
            }
        }
    }
}