namespace Main.RXs
{
    public static class TimeExtension
    {
        public static RXsTimer GetTimer(
            this IRXsObservable<IRXsTimeData> timeObservable,
            float targt,
            float time = 0,
            float scale = 1,
            TimeState state = TimeState.Play)
                => RXsTimer.GetFromReusePool(timeObservable, targt, time, scale, state);
    }
}