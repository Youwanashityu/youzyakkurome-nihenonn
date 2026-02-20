using System.Collections.Generic;

public class InventoryHandler
{
    private Dictionary<ItemType, int> _items;

    public void AddItem(ItemType type)
    {
        _items[type]++;
    }
    
    public void UseItem(ItemType type)
    {
        _items[type]--;
    }
}
