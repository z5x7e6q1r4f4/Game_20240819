namespace Main.RXs
{
    partial class RXsValueOperatorExtension
    {
        private class OfTypePropertyToPropertyOperator<T> : RXsPropertyToPropertyOperator<T>
        {
            public OfTypePropertyToPropertyOperator(IRXsProperty_Readonly source, IRXsProperty<T> result = null) : base(source, result) { }
            protected override void AfterSet(IRXsProperty_AfterSet e)
            {
                if (e.Current is T typed) Result.Value = typed;
                else Result.Value = default;
            }
        }
        private class OfTypeCollectionToCollectionOperator<T> : RXsCollectionToCollectionOperator<T>
        {
            public OfTypeCollectionToCollectionOperator(IRXsCollection_Readonly source, IRXsCollection<T> result = null) : base(source, result) { }
            protected override void AfterAdd(IRXsCollection_AfterAdd e)
            { if (e.Item is T typed) Result.Add(typed); }
            protected override void AfterRemove(IRXsCollection_AfterRemove e)
            { if (e.Item is T typed && Result.Contains(typed)) Result.Remove(typed); }
        }
        public static IOperatorToProperty<T> OfType<T>(this IRXsProperty_Readonly source, IRXsProperty<T> result = null)
            => new OfTypePropertyToPropertyOperator<T>(source, result);
        public static IOperatorToCollection<T> OfType<T>(this IRXsCollection_Readonly source, IRXsCollection<T> result = null)
            => new OfTypeCollectionToCollectionOperator<T>(source, result);
    }
}