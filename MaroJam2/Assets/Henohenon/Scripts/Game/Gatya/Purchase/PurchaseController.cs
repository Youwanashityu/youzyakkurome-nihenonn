using System.Collections.Generic;
using TMPro;

public class PurchaseController
{
    private readonly TMP_Text _keyText;
    private readonly IReadOnlyDictionary<PurchaseType, PurchaseInfo> _purchaseInfos;
    private readonly IBadMoneyKeyHandler _badMoneyKeyHandler;

    public PurchaseController(TMP_Text keyText, IReadOnlyDictionary<PurchaseType, PurchaseInfo> purchaseInfos, IBadMoneyKeyHandler badMoneyKeyHandler)
    {
        _keyText = keyText;
        _purchaseInfos = purchaseInfos;
        _badMoneyKeyHandler = badMoneyKeyHandler;
    }

    public void Purchase(PurchaseType type)
    {
        var info = _purchaseInfos[type];
        _badMoneyKeyHandler.Purchase(info.KeyAmount, info.BadMoneyAmount);
        _keyText.text = _badMoneyKeyHandler.KeyAmount.ToString();
    }
}
