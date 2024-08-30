using System.Linq;

namespace Main.RXs
{
    partial class RXsValueOperatorExtension
    {
        private class FirstOrDefaultOperator<T> : RXsCollectionToPropertyOperator<T, T>
        {
            public FirstOrDefaultOperator(IRXsCollection_Readonly<T> source, IRXsProperty<T> result = null) : base(source, result) => Subscribe();
            protected override void AfterAdd(IRXsCollection_AfterAdd<T> e)
            {
                if (e.Index == 0) Result.Value = e.Item;
            }
            protected override void AfterRemove(IRXsCollection_AfterRemove<T> e)
            {
                if (e.Index == 0) Result.Value = Source.AsEnumerable().FirstOrDefault();
            }
        }
        public static IOperatorToProperty<T> FirstOrDefault<T>(this IRXsCollection_Readonly<T> source, IRXsProperty<T> result = null)
            => new FirstOrDefaultOperator<T>(source, result);
    }
}