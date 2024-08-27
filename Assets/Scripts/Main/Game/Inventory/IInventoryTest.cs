using System.Collections.Generic;

namespace Main.Game
{
    public interface IInventoryTest
    {
        IInventoryTest Test();
        IInventoryTest Add(Item item);
        IInventoryTest AddRange(IEnumerable<Item> items);
        IInventoryTest Remove(Item item);
        IInventoryTest RemoveRange(IEnumerable<Item> items);
        bool Result();
    }
}
