
using System;
using UnityEngine;

[Serializable]
public class ItemDisplayInfo
{
    [SerializeField]
    private string displayName;
    [SerializeField]
    private Sprite icon;
    [SerializeField]
    private ItemTier tier;
    
    public ItemDisplayInfo(ItemType type, string displayName, Sprite icon, ItemTier tier)
    {
        this.displayName = displayName;
        this.icon = icon;
        this.tier = tier;
    }
    public string DisplayName => displayName;
    public Sprite Icon => icon;
    public ItemTier Tier => tier;
}