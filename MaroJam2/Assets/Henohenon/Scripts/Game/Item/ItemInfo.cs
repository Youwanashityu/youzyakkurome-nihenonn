
using System.Collections.Generic;

public class ItemInfo
{
    public readonly int InitKeyAmount;
    public IReadOnlyDictionary<ItemType, ItemDisplayInfo> DisplayInfo { get; }
    
    public ItemInfo(int initKeyAmount, Dictionary<ItemType, ItemDisplayInfo> displayInfo)
    {
        InitKeyAmount = initKeyAmount;
        DisplayInfo = displayInfo;
    }
    
}