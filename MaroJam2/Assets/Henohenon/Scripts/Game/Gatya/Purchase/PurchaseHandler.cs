using System.Collections.Generic;
using TMPro;

public class PurchaseHandler
{
    private readonly IReadOnlyDictionary<PurchaseType, PurchaseInfo> _purchaseInfos;
    private readonly IAkkaKeyHandler _akkaKeyHandler;

    public PurchaseHandler(IReadOnlyDictionary<PurchaseType, PurchaseInfo> purchaseInfos, IAkkaKeyHandler akkaKeyHandler)
    {
        _purchaseInfos = purchaseInfos;
        _akkaKeyHandler = akkaKeyHandler;
    }

    public void Purchase(PurchaseType type)
    {
        var info = _purchaseInfos[type];
        _akkaKeyHandler.Purchase(info.KeyAmount, info.AkkaAmount);
    }
}
