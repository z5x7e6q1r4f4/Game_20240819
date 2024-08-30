using Main.RXs;
using System;

namespace Main
{
    public partial class GameComponent
    {
        private class EventHandler : RXsEventHandler<GameComponent>, IObservableImmediately<GameComponent>
        {
            private class EventHandlerImmediately : RXsEventHandler<GameComponent>
            {
                private readonly GameComponent component;
                private readonly Func<GameComponent, bool> isImmediately;
                public override IDisposable Subscribe(System.IObserver<GameComponent> observer)
                {
                    if (isImmediately(component)) observer.OnNext(component);
                    return base.Subscribe(observer);
                }
                public EventHandlerImmediately(GameComponent component, Func<GameComponent, bool> isImmediately)
                {
                    this.component = component;
                    this.isImmediately = isImmediately;
                }
            }
            public EventHandler(GameComponent component, Func<GameComponent, bool> isImmediately) : base(
                observer => { if (isImmediately(component)) observer.OnNext(component); })
            { }
        }
    }
}