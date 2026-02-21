using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class GatyaHandler : IDisposable
{
    private readonly GatyaElements _elements;
    private readonly GatyaController _gatyaController;
    private readonly PurchaseController _purchaseController;
    private readonly IReadOnlyDictionary<PurchaseType, Button> _purchaseButtons;
    public IGatyaController GatyaController => _gatyaController;

    public GatyaHandler(GatyaElements elements, GatyaData data, ItemInfo itemInfo)
    {
        _elements = elements;
        _gatyaController = new GatyaController(elements, itemInfo.DisplayInfo, data.MaxTenjoCount);
        _purchaseController = new PurchaseController(elements.KeyText, data.PurchaseInfos);
        
        _elements.ChangeButton.onClick.AddListener(OnChange);
        _elements.OneButton.onClick.AddListener(_gatyaController.OnOne);
        _elements.TenButton.onClick.AddListener(_gatyaController.OnTen);
        _elements.AddKeysButton.onClick.AddListener(_elements.PurchasePopup.Show);
        _purchaseButtons = elements.PurchaseButtons;
        foreach (var purchase in _purchaseButtons)
        {
            purchase.Value.onClick.AddListener(() => _purchaseController.Purchase(purchase.Key));
        }
    }

    private void OnChange()
    {
        _elements.CharacterSelector.Popup.Show();
    }
    public void Dispose()
    {
        _elements.ChangeButton.onClick.RemoveListener(OnChange);
        _elements.OneButton.onClick.RemoveListener(_gatyaController.OnOne);
        _elements.TenButton.onClick.RemoveListener(_gatyaController.OnTen);
        _elements.AddKeysButton.onClick.RemoveListener(_elements.PurchasePopup.Show);
        foreach (var purchase in _purchaseButtons)
        {
            purchase.Value.onClick.RemoveListener(() => _purchaseController.Purchase(purchase.Key));
        }
        
        _gatyaController.Dispose();
    }
}
