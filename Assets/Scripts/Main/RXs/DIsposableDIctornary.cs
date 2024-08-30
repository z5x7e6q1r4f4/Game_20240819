using System;
using System.Collections.Generic;

namespace Main.RXs
{
    public class DisposableDictonary : IDisposable
    {
        Dictionary<object, IDisposable> Disposables = new();
        public void Add(object key, IDisposable disposable) => Disposables.Add(key, disposable);
        public IDisposable Remove(object key)
        {
            if (Disposables.TryGetValue(key, out var disposable))
            {
                Disposables.Remove(key);
                return disposable;
            }
            else return Disposiable.Empty;
        }
        public bool ContainsKey(object key) => Disposables.ContainsKey(key);
        public void Dispose(object key) => Remove(key).Dispose();
        public void Dispose()
        {
            foreach (var disposable in Disposables.Values) disposable.Dispose();
            Disposables.Clear();
        }
    }
}