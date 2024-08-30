namespace Main.RXs 
{
    partial class RXsObservable 
    {
        public static IRXsObservable<int> FromRange(int from, int to)
        {
            var dir = (to - from) < 0 ? -1 : 1;
            return FromAction<int>(o =>
            {
                for (int i = from; i != to; i += dir) o.OnNext(i);
            });
        }
    }
}