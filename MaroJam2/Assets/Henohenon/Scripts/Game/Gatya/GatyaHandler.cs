using R3;
using System;
using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

public class GatyaHandler : IDisposable
{
    private readonly GatyaElements _elements;
    private readonly IAkkaKeyHandler _akkaKeyHandler;
    private readonly GatyaController _gatyaController;
    private readonly PurchaseHandler _purchaseHandler;
    private readonly IReadOnlyDictionary<PurchaseType, Button> _purchaseButtons;
    private readonly CompositeDisposable _disposable;
    public IGatyaController GatyaController => _gatyaController;

    public GatyaHandler(GatyaElements elements, GatyaData data, ItemInfo itemInfo, InventoryKeyHandler inventoryKeyHandler)
    {
        _elements = elements;
        _akkaKeyHandler = inventoryKeyHandler;
        _gatyaController = new GatyaController(elements, itemInfo.DisplayInfo, data.MaxTenjoCount);
        _purchaseHandler = new PurchaseHandler(data.PurchaseInfos, inventoryKeyHandler);
        
        _elements.ChangeButton.onClick.AddListener(OnChange);
        _elements.OneButton.onClick.AddListener(OnOne);
        _elements.TenButton.onClick.AddListener(OnTen);
        _elements.AddKeysButton.onClick.AddListener(_elements.PurchaseController.Popup.Show);
        _purchaseButtons = elements.PurchaseController.Buttons;
        foreach (var purchase in _purchaseButtons)
        {
            purchase.Value.onClick.AddListener(() => _purchaseHandler.Purchase(purchase.Key));
        }

        _disposable = new CompositeDisposable();
        _gatyaController.OnGetItem.Subscribe(inventoryKeyHandler.AddItem).AddTo(_disposable);
        OnKeyChange(_akkaKeyHandler.KeyAmount.CurrentValue);
        OnKeyChange(_akkaKeyHandler.AkkaAmount.CurrentValue);
        _akkaKeyHandler.KeyAmount.Subscribe(OnKeyChange).AddTo(_disposable);
        _akkaKeyHandler.AkkaAmount.Subscribe(OnAkkaChange).AddTo(_disposable);
    }

    private void OnOne()
    {
        if (1 <= _akkaKeyHandler.KeyAmount.CurrentValue)
        {
            _akkaKeyHandler.UseKey(1);
            _gatyaController.GatyaOne();
        }
        else
        {
            _elements.PurchaseController.Popup.Show();
            _elements.AlertText.Show(CancellationToken.None).Forget();
        }
    }
    
    private void OnTen()
    {
        if (10 <= _akkaKeyHandler.KeyAmount.CurrentValue)
        {
            _akkaKeyHandler.UseKey(10);
            _gatyaController.GatyaTen();
        }
        else
        {
            _elements.PurchaseController.Popup.Show();
            _elements.AlertText.Show(CancellationToken.None).Forget();
        }
    }

    private void OnKeyChange(int value)
    {
        _elements.KeyText.text = value.ToString();
    }

    private void OnAkkaChange(int value)
    {
        _elements.PurchaseController.AkkaText.text = value.ToString("N0");
    }

    private void OnChange()
    {
        _elements.CharacterSelector.Popup.Show();
    }
    public void Dispose()
    {
        _elements.ChangeButton.onClick.RemoveListener(OnChange);
        _elements.OneButton.onClick.RemoveListener(_gatyaController.GatyaOne);
        _elements.TenButton.onClick.RemoveListener(_gatyaController.GatyaTen);
        _elements.AddKeysButton.onClick.RemoveListener(_elements.PurchaseController.Popup.Show);
        foreach (var purchase in _purchaseButtons)
        {
            purchase.Value.onClick.RemoveListener(() => _purchaseHandler.Purchase(purchase.Key));
        }
        
        _gatyaController.Dispose();
        _disposable.Dispose();
    }
}
