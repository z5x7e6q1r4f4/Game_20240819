namespace Main 
{
    partial class Observable 
    {
        public static ObservableFromAction<int> Range(int from, int to)
        {
            var dir = (to - from) < 0 ? -1 : 1;
            return Create<int>(o =>
            {
                for (int i = from; i != to; i += dir) o.OnNext(i);
                return o;
            });
        }
    }
}