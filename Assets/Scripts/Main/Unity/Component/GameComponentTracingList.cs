using Main.RXs;
using System;
using System.Collections.Generic;

namespace Main
{
    internal class GameComponentTracingList : GameComponent, IRXsCollection<GameComponent>
    {
        protected IRXsCollection<GameComponent> GameComponents => AwakeSelf<GameComponentTracingList>().gameComponents;
        private readonly RXsCollection_SerializeField<GameComponent> gameComponents = new();
        protected override void OnGameComponentAwake() => gameComponents.AddRange(GetComponents<GameComponent>());
        #region IRXsCollection
        public IRXsObservable<IRXsCollection_BeforeAdd<GameComponent>> BeforeAdd => GameComponents.BeforeAdd;
        public IRXsObservable<IRXsCollection_BeforeRemove<GameComponent>> BeforeRemove => GameComponents.BeforeRemove;
        public IRXsObservableImmediately<IRXsCollection_AfterAdd<GameComponent>> AfterAdd => GameComponents.AfterAdd;
        public IRXsObservable<IRXsCollection_AfterRemove<GameComponent>> AfterRemove => GameComponents.AfterRemove;
        public int Count => GameComponents.Count;
        GameComponent IRXsCollection_Readonly<GameComponent>.this[int index] => ((IRXsCollection_Readonly<GameComponent>)GameComponents)[index];
        public GameComponent this[int index] { get => GameComponents[index]; set => GameComponents[index] = value; }
        public int Add(GameComponent item, bool beforeAdd = true, bool afterAdd = true) => GameComponents.Add(item, beforeAdd, afterAdd);
        public void AddRange(IEnumerable<GameComponent> collection, bool beforeAdd = true, bool afterAdd = true) => GameComponents.AddRange(collection, beforeAdd, afterAdd);
        public int Insert(int index, GameComponent item, bool beforeAdd = true, bool afterAdd = true) => GameComponents.Insert(index, item, beforeAdd, afterAdd);
        public int Remove(GameComponent item, bool beforeRemove = true, bool afterRemove = true) => GameComponents.Remove(item, beforeRemove, afterRemove);
        public void RemoveRange(IEnumerable<GameComponent> collection, bool beforeRemove = true, bool afterRemove = true) => GameComponents.RemoveRange(collection, beforeRemove, afterRemove);
        public int RemoveAt(int index, bool beforeRemove = true, bool afterRemove = true) => GameComponents.RemoveAt(index, beforeRemove, afterRemove);
        public void Clear(bool beforeRemove = true, bool afterRemove = true) => GameComponents.Clear(beforeRemove, afterRemove);
        public bool Contains(GameComponent item) => GameComponents.Contains(item);
        public int IndexOf(GameComponent item) => GameComponents.IndexOf(item);
        public GameComponent GetAt(int index, bool indexCheck = true) => GameComponents.GetAt(index, indexCheck);
        public void SetAt(int index, GameComponent value, bool indexCheck = true, bool invokeEvent = true) => GameComponents.SetAt(index, value, indexCheck, invokeEvent);
        public IEnumerator<GameComponent> GetEnumerator() => GameComponents.GetEnumerator();
        #endregion
    }
}