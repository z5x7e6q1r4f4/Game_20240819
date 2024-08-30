using UnityEngine;
using Main.RXs;
namespace Main.Game
{
    public class InventoryInput : Inventory
    {
        public InventoryFilter Filter => AwakeSelf<InventoryInput>().filter;
        [SerializeField] private InventoryFilter filter = new();
        protected override void OnGameComponentAwake()
        {
            Items.BeforeAdd.Subscribe(Filter.FilterBeforeAdd);
            testItems.BeforeAdd.Subscribe(Filter.FilterBeforeAdd);
            base.OnGameComponentAwake();
        }
    }
}