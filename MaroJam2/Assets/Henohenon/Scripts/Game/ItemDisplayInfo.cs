
using System;
using UnityEngine;

[Serializable]
public class ItemDisplayInfo
{
    [SerializeField]
    private string displayName;
    [SerializeField]
    private Sprite icon;
    
    public ItemDisplayInfo(ItemType type, string displayName, Sprite icon)
    {
        this.displayName = displayName;
        this.icon = icon;
    }
    public string DisplayName => displayName;
    public Sprite Icon => icon;
}