
using System.Collections.Generic;

public class ItemInfo
{
    private readonly Dictionary<ItemType, ItemDisplayInfo> _displayInfo;
    
    public ItemInfo(Dictionary<ItemType, ItemDisplayInfo> displayInfo)
    {
        _displayInfo = displayInfo;
    }
    
    public Dictionary<ItemType, ItemDisplayInfo> DisplayInfo => _displayInfo;
}