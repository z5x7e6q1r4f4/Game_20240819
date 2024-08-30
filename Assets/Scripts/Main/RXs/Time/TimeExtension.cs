namespace Main.RXs
{
    public static class TimeExtension
    {
        public static Timer GetTimer(
            this IRXsObservable<ITimeData> timeObservable,
            float targt,
            float time = 0,
            float scale = 1,
            TimeState state = TimeState.Play)
                => Timer.GetFromReusePool(timeObservable, targt, time, scale, state);
    }
}