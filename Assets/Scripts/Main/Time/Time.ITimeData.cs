namespace Main
{
    partial class TimeAndUpdate
    {
        public interface ITimeData
        {
            float Time { get; }
            float Delta { get; }
        }
    }
}