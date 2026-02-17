
using System;
using UnityEngine;

[Serializable]
public class ItemDisplayInfo
{
    [SerializeField]
    private ItemType type;
    [SerializeField]
    private string displayName;
    [SerializeField]
    private Sprite icon;
    
    public ItemDisplayInfo(ItemType type, string displayName, Sprite icon)
    {
        this.type = type;
        this.displayName = displayName;
        this.icon = icon;
    }
    public ItemType Type => type;
    public string DisplayName => displayName;
    public Sprite Icon => icon;
}