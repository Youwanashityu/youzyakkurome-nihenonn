using UnityEngine;

public class GatyaTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private GatyaElements elements;
    [SerializeField] private GatyaTableScriptable luxTable;
    [SerializeField] private ItemInfoScriptable itemInfo;

    private GatyaHandler _handler;

    private void Awake()
    {
        _handler = new GatyaHandler(elements, luxTable.GetPureData, itemInfo.GetPureData);
    }

    private void OnDestroy()
    {
        _handler.Dispose();
    }
}
