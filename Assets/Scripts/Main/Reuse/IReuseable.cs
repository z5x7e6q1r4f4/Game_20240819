namespace Main
{
    public interface IReuseable
    {
        IReusePool Pool { get; set; }
        public interface IOnGet : IReuseable { void OnGet(); }
        public interface IOnRelease : IReuseable { void OnRelease(); }
        public interface IOnDestroy : IReuseable { void OnDestroy(); }
    }
}