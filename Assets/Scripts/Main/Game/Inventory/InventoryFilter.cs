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
        public PropertySerializeField<FilterMode> Mode { get; private set; } = new();
        public CollectionSerializeField<Item> Items { get; private set; } = new();
        public void FilterBeforeAdd(CollectionBeforeAdd<Item> e) => e.IsEnable = IsEnable(e.Item);
        public void FilterBeforeRemove(CollectionBeforeRemove<Item> e) => e.IsEnable = IsEnable(e.Item);
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