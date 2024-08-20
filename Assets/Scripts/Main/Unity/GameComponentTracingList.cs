using Main.RXs;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Main
{
    internal class GameComponentTracingList : GameComponent, IRXsCollection<GameComponent>
    {
        private RXsCollection_SerializeField<GameComponent> gameComponents = new();
        GameComponent IRXsCollection_Readonly<GameComponent>.this[int index] => gameComponents[index];
        IObservableImmediately<IRXsCollection_AfterAdd<GameComponent>> IRXsCollection_Readonly<GameComponent>.AfterAdd => gameComponents.AfterAdd;
        IObservable<IRXsCollection_AfterRemove<GameComponent>> IRXsCollection_Readonly<GameComponent>.AfterRemove => gameComponents.AfterRemove;
        public int Count => gameComponents.Count;
        public IObservable<IRXsCollection_BeforeAdd<GameComponent>> BeforeAdd => gameComponents.BeforeAdd;
        public IObservable<IRXsCollection_BeforeRemove<GameComponent>> BeforeRemove => gameComponents.BeforeRemove;
        public GameComponent this[int index] { get => gameComponents[index]; set => gameComponents[index] = value; }
        bool IRXsCollection_Readonly<GameComponent>.Contains(GameComponent item) => gameComponents.Contains(item);
        public IEnumerator<GameComponent> GetEnumerator() => ((IEnumerable<GameComponent>)gameComponents).GetEnumerator();
        int IRXsCollection_Readonly<GameComponent>.IndexOf(GameComponent item) => gameComponents.IndexOf(item);
        public int Add(GameComponent item, bool beforeAdd = true, bool afterAdd = true) => gameComponents.Add(item, beforeAdd, afterAdd);
        public void AddRange(IEnumerable<GameComponent> collection, bool beforeAdd = true, bool afterAdd = true) => gameComponents.AddRange(collection, beforeAdd, afterAdd);
        public int Insert(int index, GameComponent item, bool beforeAdd = true, bool afterAdd = true) => gameComponents.Insert(index, item, beforeAdd, afterAdd);
        public int Remove(GameComponent item, bool beforeRemove = true, bool afterRemove = true) => gameComponents.Remove(item, beforeRemove, afterRemove);
        public void Clear(bool beforeRemove = true, bool afterRemove = true) => gameComponents.Clear(beforeRemove, afterRemove);
        public int RemoveAt(int index, bool beforeRemove = true, bool afterRemove = true) => gameComponents.RemoveAt(index, beforeRemove, afterRemove);
    }
}