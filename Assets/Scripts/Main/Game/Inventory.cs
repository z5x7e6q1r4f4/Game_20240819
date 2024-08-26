using Main.RXs;
using UnityEngine;

namespace Main.Game
{
    public class Inventory : BodyPartComponent
    {
        public RXsCollection_SerializeField<Item> Items { get; } = new();
        public RXsProperty_SerializeField<int> Capacity { get; } = new();
        public int Remain => Capacity.Value - Items.Count;
        protected override void OnGameComponentAwake()
        {
            Items.BeforeAdd.Subscribe(e =>
            {
                if (e.Index >= Capacity.Value) e.IsEnable = false;
            });
        }
    }
}
