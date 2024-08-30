using System;

namespace Main.RXs
{
    public static partial class RXsValueOperatorExtension
    {
        private abstract class RXsPropertyToOperator<TSource> : IDisposable
        {
            protected virtual int Order => default;
            protected IRXsProperty_Readonly<TSource> Source { get; }
            private IDisposable disposable;
            public RXsPropertyToOperator(IRXsProperty_Readonly<TSource> source) => Source = source;
            public virtual void Subscribe()
            {
                if (disposable != null) return;
                disposable = Source.AfterSet.Immediately().Order(Order).Subscribe(AfterSet);
            }
            public virtual void Dispose()
            {
                disposable.Dispose();
                disposable = null;
            }
            protected abstract void AfterSet(IRXsProperty_AfterSet<TSource> e);
        }
        private abstract class RXsPropertyToOperator : IDisposable
        {
            protected virtual int Order => default;
            protected IRXsProperty_Readonly Source { get; }
            private IDisposable disposable;
            public RXsPropertyToOperator(IRXsProperty_Readonly source) => Source = source;
            public virtual void Subscribe()
            {
                if (disposable != null) return;
                disposable = Source.AfterSet.Immediately().Order(Order).Subscribe(AfterSet);
            }
            public virtual void Dispose()
            {
                disposable.Dispose();
                disposable = null;
            }
            protected abstract void AfterSet(IRXsProperty_AfterSet e);
        }
    }
}