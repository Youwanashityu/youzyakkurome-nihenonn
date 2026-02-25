using System;
using R3;
using UnityEngine;

public class GatyaTestEntryPoint : MonoBehaviour
{
    [SerializeField]
    private GatyaElements elements;
    [SerializeField]
    private GatyaDataScriptable gatyaData;
    [SerializeField]
    private ItemInfoScriptable itemInfo;
    [SerializeField]
    private CharacterType type;

    private GatyaHandler _handler;

    private void Awake()
    {
        var gatyaPureData = gatyaData.GetPureData;
        var itemPureData = itemInfo.GetPureData;
        _handler = new GatyaHandler(elements, gatyaPureData, itemPureData, new InventoryKeyHandler(itemInfo.GetPureData, null, new ()), new Subject<CharacterType>());
        _handler.GatyaController.Initialize(gatyaPureData.Tables[type]);
    }

    private void OnDestroy()
    {
        _handler.Dispose();
    }

    private void OnValidate()
    {
        var tables = gatyaData.GetPureData.Tables;
        _handler.GatyaController.Initialize(tables[type]);
    }
}
