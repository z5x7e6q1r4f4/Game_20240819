using System;

namespace Main.RXs
{
    partial class RXsValueOperatorExtension
    {
        private class WhereCollectionToCollectionOperator<T> : RXsCollectionToCollectionOperator<T, T>
        {
            private readonly Predicate<T> predicate;
            public WhereCollectionToCollectionOperator(IRXsCollection_Readonly<T> source, Predicate<T> predicate, IRXsCollection<T> result = null) : base(source, result)
            {
                this.predicate = predicate;
                Subscribe();
            }
            protected override void AfterAdd(IRXsCollection_AfterAdd<T> e)
            { if (predicate(e.Item)) Result.Add(e.Item); }
            protected override void AfterRemove(IRXsCollection_AfterRemove<T> e)
            { if (Result.Contains(e.Item)) Result.Remove(e.Item); }
        }
        public static IOperatorToCollection<T> Where<T>(this IRXsCollection_Readonly<T> source, Predicate<T> predicate, IRXsCollection<T> result = null)
            => new WhereCollectionToCollectionOperator<T>(source, predicate, result);
    }
}