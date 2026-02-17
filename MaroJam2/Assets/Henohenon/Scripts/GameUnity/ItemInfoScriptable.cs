using UnityEngine;

[CreateAssetMenu(
    fileName = "NewItemInfo",
    menuName = "Data/ItemInfo"
)]
public class ItemInfoScriptable : ScriptableObject
{
    [SerializeField]
    private SerializedDictionary<ItemType, ItemDisplayInfo> info;
}