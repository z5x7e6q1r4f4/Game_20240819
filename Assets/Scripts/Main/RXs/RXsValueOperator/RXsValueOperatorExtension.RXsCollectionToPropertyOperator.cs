namespace Main.RXs
{
    public static partial class RXsValueOperatorExtension
    {
        private abstract class RXsCollectionToPropertyOperator<TSource, TResult> :
            RXsCollectionToOperator<TSource>,
            IOperatorToProperty<TResult>
        {
            protected IRXsProperty<TResult> Result { get; }
            TResult IRXsProperty_Readonly<TResult>.Value => Result.Value;
            IObservableImmediately<IRXsProperty_AfterSet<TResult>> IRXsProperty_Readonly<TResult>.AfterSet => Result.AfterSet;
            protected RXsCollectionToPropertyOperator(IRXsCollection_Readonly<TSource> source, IRXsProperty<TResult> result = null) : base(source) => Result = result ?? new RXsProperty_SerializeField<TResult>();
        }
        private abstract class RXsCollectionToPropertyOperator<TResult> :
            RXsCollectionToOperator,
            IOperatorToProperty<TResult>
        {
            protected IRXsProperty<TResult> Result { get; }
            TResult IRXsProperty_Readonly<TResult>.Value => Result.Value;
            IObservableImmediately<IRXsProperty_AfterSet<TResult>> IRXsProperty_Readonly<TResult>.AfterSet => Result.AfterSet;
            protected RXsCollectionToPropertyOperator(IRXsCollection_Readonly source, IRXsProperty<TResult> result = null) : base(source) => Result = result ?? new RXsProperty_SerializeField<TResult>();
        }
    }
}