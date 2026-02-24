using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(
    fileName = "NewItemInfo",
    menuName = "Data/ItemInfo"
)]
public class ItemInfoScriptable : ScriptableObject
{
    [SerializeField]
    private int[] initKeyAmount = { 0, 12, 16, 28, 37, 53, 102, 777 };
    [SerializeField]
    private int[] initAkkaAmount = { 0, 0, 0, 250, 2500, 2500, 10000, 30000, 777777 };
    [FormerlySerializedAs("info")] [SerializeField]
    private SerializedDictionary<ItemType, ItemDisplayInfo> displayInfo;
    
    public ItemInfo GetPureData => new ItemInfo(initKeyAmount, initAkkaAmount, displayInfo);
}