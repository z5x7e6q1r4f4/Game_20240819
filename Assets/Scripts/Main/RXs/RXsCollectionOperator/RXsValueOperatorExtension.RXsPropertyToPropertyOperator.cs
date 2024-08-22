namespace Main.RXs
{
    public static partial class RXsValueOperatorExtension
    {
        private abstract class RXsPropertyToPropertyOperator<TSource, TResult> :
            RXsPropertyToOperator<TSource>,
            IOperatorToProperty<TResult>
        {
            protected IRXsProperty<TResult> Result { get; }
            TResult IRXsProperty_Readonly<TResult>.Value => Result.Value;
            IObservableImmediately<IRXsProperty_AfterSet<TResult>> IRXsProperty_Readonly<TResult>.AfterSet => Result.AfterSet;
            protected RXsPropertyToPropertyOperator(IRXsProperty_Readonly<TSource> source, IRXsProperty<TResult> result = null) : base(source) => Result = result ?? new RXsProperty_SerializeField<TResult>();
        }
        private abstract class RXsPropertyToPropertyOperator<TResult> :
            RXsPropertyToOperator,
            IOperatorToProperty<TResult>
        {
            protected IRXsProperty<TResult> Result { get; }
            TResult IRXsProperty_Readonly<TResult>.Value => Result.Value;
            IObservableImmediately<IRXsProperty_AfterSet<TResult>> IRXsProperty_Readonly<TResult>.AfterSet => Result.AfterSet;
            protected RXsPropertyToPropertyOperator(IRXsProperty_Readonly source, IRXsProperty<TResult> result = null) : base(source) => Result = result ?? new RXsProperty_SerializeField<TResult>();
        }
    }
}