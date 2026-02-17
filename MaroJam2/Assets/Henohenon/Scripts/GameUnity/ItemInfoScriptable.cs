using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(
    fileName = "NewItemInfo",
    menuName = "Data/ItemInfo"
)]
public class ItemInfoScriptable : ScriptableObject
{
    [FormerlySerializedAs("info")] [SerializeField]
    private SerializedDictionary<ItemType, ItemDisplayInfo> displayInfo;
    
    public ItemInfo GetPureData => new ItemInfo(displayInfo);
}