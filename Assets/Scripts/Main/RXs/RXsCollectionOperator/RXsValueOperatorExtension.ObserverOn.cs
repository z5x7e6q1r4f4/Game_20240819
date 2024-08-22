using System;

namespace Main.RXs
{
    partial class RXsValueOperatorExtension
    {
        private class ObserverOnPropertyToPropertyOperator<TSource, TResult> :
            RXsPropertyToPropertyOperator<TSource, TResult>
        {
            private readonly Func<TSource, IRXsProperty_Readonly<TResult>> func;
            private IDisposable subcription;
            public ObserverOnPropertyToPropertyOperator(IRXsProperty_Readonly<TSource> source, Func<TSource, IRXsProperty_Readonly<TResult>> func, IRXsProperty<TResult> result = null) : base(source, result)
            {
                this.func = func;
                Subscribe();
            }
            protected override void AfterSet(IRXsProperty_AfterSet<TSource> e)
            {
                subcription?.Dispose();
                subcription = null;
                if (e.Current == null) return;
                subcription = func(e.Current).ConnectTo(Result);
            }
        }
        private class ObserverOnPropertyToCollectionOperator<TSource, TResult> :
            RXsPropertyToCollectionOperator<TSource, TResult>
        {
            private readonly Func<TSource, IRXsCollection_Readonly<TResult>> func;
            private IDisposable subcription;
            public ObserverOnPropertyToCollectionOperator(IRXsProperty_Readonly<TSource> source, Func<TSource, IRXsCollection_Readonly<TResult>> func, IRXsCollection<TResult> result = null) : base(source, result)
            {
                this.func = func;
                Subscribe();
            }
            protected override void AfterSet(IRXsProperty_AfterSet<TSource> e)
            {
                subcription?.Dispose();
                subcription = null;
                if (e.Current == null) return;
                subcription = func(e.Current).ConnectTo(Result);
            }
        }
        public static IOperatorToCollection<TResult> ObserverOn<TSource, TResult>(
            this IRXsProperty_Readonly<TSource> source,
            Func<TSource, IRXsCollection_Readonly<TResult>> func,
            IRXsCollection<TResult> result = null)
                => new ObserverOnPropertyToCollectionOperator<TSource, TResult>(source, func, result);
        public static IOperatorToProperty<TResult> ObserverOn<TSource, TResult>(
            this IRXsProperty_Readonly<TSource> source,
            Func<TSource, IRXsProperty_Readonly<TResult>> func,
            IRXsProperty<TResult> result = null)
                => new ObserverOnPropertyToPropertyOperator<TSource, TResult>(source, func, result);
    }
}