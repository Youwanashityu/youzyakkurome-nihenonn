using System;
using System.Collections.Generic;
using R3;
using UnityEngine;

public class InventoryKeyHandler: IItemsHandler, IBadMoneyKeyHandler, IDisposable
{
    private readonly IReadOnlyDictionary<ItemType, ItemDisplayInfo> _displayInfos;
    private readonly PresentsPopupController _presentsPopup;
    private readonly Dictionary<ItemType, int> _items;
    private readonly ReactiveProperty<int> _keyAmount;
    private readonly ReactiveProperty<int> _badMoneyAmount;
    private readonly IDisposable _subscription;
    public ReadOnlyReactiveProperty<int> KeyAmount => _keyAmount;
    public ReadOnlyReactiveProperty<int> BadMoneyAmount => _badMoneyAmount;

    public InventoryKeyHandler(ItemInfo itemInfo, Observable<ItemType> onGetItem, PresentsPopupController presentsPopup)
    {
        _displayInfos = itemInfo.DisplayInfo;
        _keyAmount = new ReactiveProperty<int>(itemInfo.InitKeyAmount);
        _badMoneyAmount = new ReactiveProperty<int>(0);
        _presentsPopup = presentsPopup;
        _items = new();

        _subscription = onGetItem.Subscribe(AddItem);
    }

    private void AddItem(ItemType type)
    {
        _items[type] = _items.GetValueOrDefault(type) + 1;
        _presentsPopup.Set(type, _displayInfos[type], _items[type]);
    }
    
    public int UseItem(ItemType type)
    {
        if (!_items.TryGetValue(type, out var result)) return 0;
        _items[type] = 0;
        _presentsPopup.Set(type, _displayInfos[type], _items[type]);

        return result;
    }

    public void Purchase(int addKey, int subBadMoney)
    {
        _keyAmount.Value += addKey;
        _badMoneyAmount.Value -= subBadMoney;
    }

    public void Dispose()
    {
        _subscription.Dispose();
    }
}
