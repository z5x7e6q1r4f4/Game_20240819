using System;
using Main.RXs.RXsProperties;

namespace Main.RXs
{

    public abstract partial class ObservablePropertyBase<T>
    {
        private class EventArgs :
            Reuse.ObjectBase<EventArgs>,
            IObservableProperty_BeforeSet<T>,
            IObservableProperty_AfterSet<T>,
            IDisposable
        {
            public ObservablePropertyEventArgsType Type { get; private set; }
            public IObservableProperty_Readonly<T> Property { get; private set; }
            public bool IsEnable { get; set; }
            public T Previous { get; private set; }
            public T Current { get; private set; }
            public T Modified { get; set; }
            public static EventArgs GetFromReusePool(IObservableProperty_Readonly<T> property, ObservablePropertyEventArgsType type, T previous, T current)
            {
                var eventArgs = GetFromReusePool();
                eventArgs.Property = property;
                eventArgs.Type = type;
                eventArgs.IsEnable = true;
                eventArgs.Previous = previous;
                eventArgs.Current = current;
                eventArgs.Modified = current;
                return eventArgs;
            }
            public void Dispose() => this.ReleaseToReusePool();
            public override string ToString()
            {
                string type = $"<color=yellow>{Type}</color> ";
                string isEnable = $"IsEnable:{(IsEnable ? "<color=green>Enable</color>" : "<color=red>Disable</color>")},";
                string previous = $"Previous:<color=green>{Previous}</color>,";
                string current = $"Current:<color=green>{Current}</color>";
                string modified = !Equals(Modified, Current) ? $",Modified:<color=yellow>{Modified}</color>" : string.Empty;
                return Type switch
                {
                    ObservablePropertyEventArgsType.BeforeSet => $"{type}{isEnable}{previous}{current}{modified}",
                    ObservablePropertyEventArgsType.AfterSet => $"{type}{previous}{current}",
                    ObservablePropertyEventArgsType.BeforeGet => $"{type}{isEnable}{previous}{current}{modified}",
                    _ => throw new Exception(),
                };
            }
        }
    }
}