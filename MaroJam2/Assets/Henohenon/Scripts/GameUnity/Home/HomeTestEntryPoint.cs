using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using Henohenon.Scripts.GameUnity.General;
using R3;
using UnityEngine;

public class HomeTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private HomeElements homeElements;
    [SerializeField]
    private GeneralElements generalElements;
    [SerializeField]
    private ItemInfoScriptable itemInfo;
    [SerializeField]
    private SerializedDictionary<ItemType, int> initItems;
    
    private HomeHandler _handler;
    private Subject<ItemType> _onGetItem;

    private void Awake()
    {
        var inventoryHandler = new InventoryKeyHandler(itemInfo.GetPureData, homeElements.Presents, initItems);
        _handler = new HomeHandler(homeElements, inventoryHandler);
    }

    private void OnDestroy()
    {
        _handler.Dispose();
        _onGetItem.Dispose();
    }
}
