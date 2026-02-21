using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class InventoryKeyHandler: IItemsHandler, IAkkaKeyHandler, IDisposable
{
    private readonly IReadOnlyDictionary<ItemType, ItemDisplayInfo> _displayInfos;
    private readonly PresentsPopupController _presentsPopup;
    private readonly Dictionary<ItemType, int> _items;
    private readonly ReactiveProperty<int> _keyAmount;
    private readonly ReactiveProperty<int> _akkaAmount;
    public ReadOnlyReactiveProperty<int> KeyAmount => _keyAmount;
    public ReadOnlyReactiveProperty<int> AkkaAmount => _akkaAmount;

    public InventoryKeyHandler(ItemInfo itemInfo, PresentsPopupController presentsPopup)
    {
        _displayInfos = itemInfo.DisplayInfo;
        var randomKeyAmount =  itemInfo.InitKeyAmount[UnityEngine.Random.Range(0, itemInfo.InitKeyAmount.Length)];
        var randomAkkaAmount =  itemInfo.InitAkkaAmount[UnityEngine.Random.Range(0, itemInfo.InitAkkaAmount.Length)];
        _keyAmount = new ReactiveProperty<int>(randomKeyAmount);
        _akkaAmount = new ReactiveProperty<int>(randomAkkaAmount);
        _presentsPopup = presentsPopup;
        _items = new();
    }

    public void AddItem(ItemType type)
    {
        _items[type] = _items.GetValueOrDefault(type) + 1;
        _presentsPopup.Set(type, _displayInfos[type], _items[type]);
    }
    
    public int UseItem(ItemType type)
    {
        if (!_items.TryGetValue(type, out var result)) return 0;
        _items[type] = 0;
        _presentsPopup.Set(type, _displayInfos[type], _items[type]);
        _presentsPopup.Popup.Hide();

        return result;
    }

    public void Purchase(int addKey, int subAkka)
    {
        _keyAmount.Value = _keyAmount.CurrentValue + addKey;
        _akkaAmount.Value = _akkaAmount.CurrentValue - subAkka;
    }

    public void UseKey(int numb)
    {
        _keyAmount.Value = _keyAmount.CurrentValue - numb;
    }

    public void Dispose()
    {
    }
}
