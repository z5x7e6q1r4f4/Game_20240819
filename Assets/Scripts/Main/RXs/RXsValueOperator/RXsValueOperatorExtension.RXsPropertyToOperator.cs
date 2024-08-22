using System;

namespace Main.RXs
{
    public static partial class RXsValueOperatorExtension
    {
        private abstract class RXsPropertyToOperator<TSource> : IDisposable
        {
            protected IRXsProperty_Readonly<TSource> Source { get; }
            private IDisposable disposable;
            public RXsPropertyToOperator(IRXsProperty_Readonly<TSource> source) => Source = source;
            public virtual void Subscribe()
            {
                if (disposable != null) return;
                disposable = Source.AfterSet.Immediately().Subscribe(AfterSet);
            }
            public virtual void Dispose()
            {
                if (disposable == null) return;
                using var eventArgs = EventArgs.GetFromReusePool(Source, Source.Value, default);
                AfterSet(eventArgs);
                disposable.Dispose();
                disposable = null;
            }
            protected abstract void AfterSet(IRXsProperty_AfterSet<TSource> e);
            private class EventArgs : Reuse.ObjectBase<EventArgs>, IRXsProperty_AfterSet<TSource>, IDisposable
            {
                public IRXsProperty_Readonly<TSource> Property { get; private set; }
                public TSource Previous { get; private set; }
                public TSource Current { get; private set; }
                public static EventArgs GetFromReusePool(IRXsProperty_Readonly<TSource> property, TSource previous, TSource current)
                {
                    var eventArgs = GetFromReusePool();
                    eventArgs.Property = property;
                    eventArgs.Previous = previous;
                    eventArgs.Current = current;
                    return eventArgs;
                }
                void IDisposable.Dispose() => this.ReleaseToReusePool();
            }
        }
        private abstract class RXsPropertyToOperator : IDisposable
        {
            protected IRXsProperty_Readonly Source { get; }
            private IDisposable disposable;
            public RXsPropertyToOperator(IRXsProperty_Readonly source) => Source = source;
            public virtual void Subscribe()
            {
                if (disposable != null) return;
                disposable = Source.AfterSet.Immediately().Subscribe(AfterSet);
            }
            public virtual void Dispose()
            {
                if (disposable == null) return;
                using var eventArgs = EventArgs.GetFromReusePool(Source, Source.Value, default);
                AfterSet(eventArgs);
                disposable.Dispose();
                disposable = null;
            }
            protected abstract void AfterSet(IRXsProperty_AfterSet e);
            private class EventArgs : Reuse.ObjectBase<EventArgs>, IRXsProperty_AfterSet, IDisposable
            {
                public IRXsProperty_Readonly Property { get; private set; }
                public object Previous { get; private set; }
                public object Current { get; private set; }
                public static EventArgs GetFromReusePool(IRXsProperty_Readonly property, object previous, object current)
                {
                    var eventArgs = GetFromReusePool();
                    eventArgs.Property = property;
                    eventArgs.Previous = previous;
                    eventArgs.Current = current;
                    return eventArgs;
                }
                void IDisposable.Dispose() => this.ReleaseToReusePool();
            }
        }
    }
}