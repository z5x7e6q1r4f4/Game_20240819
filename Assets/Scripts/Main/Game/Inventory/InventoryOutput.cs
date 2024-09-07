using UnityEngine;
namespace Main.Game
{
    public class InventoryOutput : Inventory
    {
        public InventoryFilter Filter => AwakeSelf<InventoryOutput>().filter;
        [SerializeField] private InventoryFilter filter = new();
        protected override void OnGameComponentAwake()
        {
            Items.BeforeRemove.Subscribe(Filter.FilterBeforeRemove);
            testItems.BeforeRemove.Subscribe(Filter.FilterBeforeRemove);
            base.OnGameComponentAwake();
        }
    }
}