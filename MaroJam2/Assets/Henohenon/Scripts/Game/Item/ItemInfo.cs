
using System.Collections.Generic;

public class ItemInfo
{
    public readonly int[] InitKeyAmount;
    public readonly int[] InitAkkaAmount;
    public IReadOnlyDictionary<ItemType, ItemDisplayInfo> DisplayInfo { get; }
    
    public ItemInfo(int[] initKeyAmount, int[] initAkkaAmount, Dictionary<ItemType, ItemDisplayInfo> displayInfo)
    {
        InitKeyAmount = initKeyAmount;
        DisplayInfo = displayInfo;
        InitAkkaAmount = initAkkaAmount;
    }
    
}