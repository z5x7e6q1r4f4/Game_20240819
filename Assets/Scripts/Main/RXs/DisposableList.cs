using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public interface IDisposableList : IDisposable
    {
        public int Count { get; }
        void Add(IDisposable disposable);
        void Remove(IDisposable disposable);
    }

    public class DisposableList : IDisposable, IDisposableList
    {
        private readonly List<IDisposable> disposables = new();
        public int Count => disposables.Count;
        public void Add(IDisposable disposable) => disposables.Add(disposable);
        public void Remove(IDisposable disposable) => disposables.Remove(disposable);
        public void Dispose() { foreach (var disposable in disposables) disposable.Dispose(); disposables.Clear(); }
        public DisposableList(params IDisposable[] disposables) { foreach (var disposable in disposables) Add(disposable); }
    }
}