using UnityEngine;

public class GatyaTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private GatyaElements elements;
    [SerializeField] private GatyaDataScriptable gatyaData;
    [SerializeField] private ItemInfoScriptable itemInfo;

    private GatyaHandler _handler;

    private void Awake()
    {
        _handler = new GatyaHandler(elements, gatyaData.GetPureData, itemInfo.GetPureData, new InventoryKeyHandler(itemInfo.GetPureData, null, new ()));
    }

    private void OnDestroy()
    {
        _handler.Dispose();
    }
}
