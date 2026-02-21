using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(
    fileName = "NewGatyaData",
    menuName = "Data/GatyaData"
)]
public class GatyaDataScriptable: ScriptableObject
{
    [SerializeField] private int maxTenjoCount = 30;
    [SerializeField] private SerializedDictionary<CharacterType, GatyaTableScriptable> tables;
    [SerializeField] private SerializedDictionary<PurchaseType, PurchaseInfo> purchaseInfos;

    public GatyaData GetPureData => new GatyaData(maxTenjoCount, tables.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.GetPureData), purchaseInfos);
}