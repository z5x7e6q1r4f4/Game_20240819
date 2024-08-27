using Main.RXs;
using System;

namespace Main.Game
{
    [Serializable]
    public class InventoryFilter
    {
        public enum FilterMode
        {
            None,
            Enable,
            Disable,
        }
        public RXsProperty_SerializeField<FilterMode> Mode { get; private set; } = new();
        public RXsCollection_SerializeField<Item> Items { get; private set; } = new();
        public void FilterBeforeAdd(IRXsCollection_BeforeAdd<Item> e) => e.IsEnable = IsEnable(e.Item);
        public void FilterBeforeRemove(IRXsCollection_BeforeRemove<Item> e) => e.IsEnable = IsEnable(e.Item);
        private bool IsEnable(Item item)
        {
            switch (Mode.Value)
            {
                case FilterMode.Enable:
                    return Items.Contains(item);
                case FilterMode.Disable:
                    return !Items.Contains(item);
            }
            return true;
        }
    }
}