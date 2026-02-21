using System.Collections.Generic;
using UnityEngine;

public class GatyaData
{
    public readonly int MaxTenjoCount;
    public IReadOnlyDictionary<CharacterType, GatyaTable> Tables;
    public IReadOnlyDictionary<PurchaseType, PurchaseInfo> PurchaseInfos;

    public GatyaData(int maxTenjoCount, IReadOnlyDictionary<CharacterType, GatyaTable> tables,
        IReadOnlyDictionary<PurchaseType, PurchaseInfo> purchaseInfos)
    {
        MaxTenjoCount = maxTenjoCount;
        Tables = tables;
        PurchaseInfos = purchaseInfos;
    }
}