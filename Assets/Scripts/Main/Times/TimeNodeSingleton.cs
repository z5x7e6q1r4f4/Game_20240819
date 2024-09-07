namespace Main
{
    public abstract class TimeNodeSingleton<TName> : TimeNode
        where TName : TimeNodeSingleton<TName>
    {
        public static TName TimeNode => timeNode ??= FindFirstObjectByType<TName>();
        private static TName timeNode;
        protected override void OnGameComponentDestroy() => timeNode = null;
    }
}